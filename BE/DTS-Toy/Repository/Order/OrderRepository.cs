using Business.DTO.Order;
using Business.Response;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Repository.Order;
using Business.Helper;

namespace Repository.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderRepository(ECommerceDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response> PlaceOrderAsync(Guid productId, OrderRequestDTO orderRequest)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Lấy UserId từ token
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    return new Response
                    {
                        Code = ResponseCode.UnAuthorized,
                        Message = "Người dùng chưa đăng nhập hoặc không hợp lệ."
                    };
                }

                // Kiểm tra sản phẩm từ ProductId
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ID == productId);
                if (product == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Sản phẩm không tồn tại."
                    };
                }

                if (product.Quantity < orderRequest.OrderDetails.Sum(d => d.Quantity))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Sản phẩm không đủ số lượng trong kho."
                    };
                }

                // Tạo Order
                var order = new Data.Models.Order
                {
                    ID = Guid.NewGuid(),
                    UserID = userId,
                    OrderDate = DateTime.UtcNow,
                    Status = "Pending"
                };
                _dbContext.Orders.Add(order);

                // Thêm OrderDetails
                decimal totalPrice = 0;
                foreach (var detail in orderRequest.OrderDetails)
                {
                    var unitPrice = (product.Price > 0 ? product.Price : product.PrevPrice) ?? 0;
                    var orderDetail = new OrderDetail
                    {
                        ID = Guid.NewGuid(),
                        OrderID = order.ID,
                        ProductID = productId,
                        Quantity = detail.Quantity,
                        UnitPrice = unitPrice
                    };
                    _dbContext.OrderDetails.Add(orderDetail);

                    // Tính tổng giá
                    totalPrice += detail.Quantity * unitPrice;

                    // Giảm số lượng sản phẩm
                    product.Quantity -= detail.Quantity;

                    // Tạo Payment cho OrderDetail
                    var payment = new Payment
                    {
                        ID = Guid.NewGuid(),
                        TotalPrice = detail.Quantity * unitPrice,
                        PaymentMethod = null, // Có thể cập nhật sau
                        PaymentCode = null, // Có thể cập nhật sau
                        UserPaymentId = userId,
                        PaymentEndDate = null,
                        OrderDetailID = orderDetail.ID,
                        OrderDetail = orderDetail,
                        Status = PaymentStatus.Pending
                    };
                    _dbContext.Payments.Add(payment);
                }

                // Lưu vào CSDL
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Đặt hàng thành công.",
                    Data = new
                    {
                        OrderID = order.ID,
                        TotalPrice = totalPrice
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Lỗi khi lưu dữ liệu: " + ex.InnerException?.Message
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Đã xảy ra lỗi trong quá trình đặt hàng: " + ex.Message
                };
            }
        }




        public async Task<PaginatedList<OrderDTO>> GetAllOrdersAsync(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var query = _dbContext.Orders
                    .Include(o => o.Users)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .Select(o => new OrderDTO
                    {
                        OrderId = o.ID,
                        OrderDate = o.OrderDate,
                        Status = o.Status,
                        UserId = o.UserID,
                        OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                        {
                            ProductId = od.ProductID ?? Guid.Empty,
                            Quantity = od.Quantity ?? 0,
                            UnitPrice = od.UnitPrice ?? 0m
                        }).ToList()
                    });

                var totalRecords = await query.CountAsync();

                var orders = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<OrderDTO>(orders, totalRecords, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách đơn hàng: {ex.Message}");
            }
        }


        // Lấy chi tiết đơn hàng theo ID
        public async Task<OrderDTO?> GetOrderByIdAsync(Guid orderId)
        {
            try
            {
                var order = await _dbContext.Orders
                    .Include(o => o.Users) // Bao gồm người dùng liên quan
                    .FirstOrDefaultAsync(o => o.ID == orderId);

                if (order == null) return null;

                return new OrderDTO
                {
                    OrderId = order.ID,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    UserId = order.UserID,
                    OrderDetails = await _dbContext.OrderDetails
                        .Where(od => od.OrderID == order.ID)
                        .Select(od => new OrderDetailDTO
                        {
                            ProductId = od.ProductID ?? Guid.Empty, // Sử dụng Guid.Empty nếu ProductID null
                            Quantity = od.Quantity ?? 0,           // Sử dụng 0 nếu Quantity null
                            UnitPrice = od.UnitPrice ?? 0m         // Sử dụng 0 nếu UnitPrice null
                        }).ToListAsync()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin đơn hàng: {ex.Message}");
            }
        }

        public async Task<List<OrderDTO>> GetOrdersByUserIdAsync(Guid userId)
        {
            try
            {
                var orders = await _dbContext.Orders
                    .Where(o => o.UserID == userId) // Lọc theo UserID
                    .Select(o => new OrderDTO
                    {
                        OrderId = o.ID,
                        OrderDate = o.OrderDate,
                        Status = o.Status,
                        UserId = o.UserID,
                        OrderDetails = _dbContext.OrderDetails
                            .Where(od => od.OrderID == o.ID)
                            .Select(od => new OrderDetailDTO
                            {
                                ProductId = od.ProductID ?? Guid.Empty, // Giá trị mặc định nếu null
                                Quantity = od.Quantity ?? 0,
                                UnitPrice = od.UnitPrice ?? 0m
                            }).ToList()
                    })
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách đơn hàng: {ex.Message}");
            }
        }


    }
}
