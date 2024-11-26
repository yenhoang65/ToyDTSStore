using AutoMapper;
using Business.DTO.Address;
using Business.DTO.CategoryDTO;
using Business.DTO.Payment;
using Business.Helper;
using Business.Response;
using Business.Validation;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Repository.PaymentRepository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ECommerceDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public PaymentRepository(
            ECommerceDBContext context,
            IMapper mapper,
            IHttpContextAccessor accessor,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _accessor = accessor;
            _configuration = configuration;
        }


        private Guid GetUserIdFromClaims()
        {
            var userIdClaim = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User ID không tồn tại trong Claims. Vui lòng đăng nhập lại hoặc kiểm tra thông tin xác thực.");
            }

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                throw new Exception("User ID không hợp lệ. Vui lòng kiểm tra thông tin xác thực.");
            }

            return userId;
        }


        public async Task<Response> GetPaymentsByUserIdAsync(int pageIndex, int pageSize, PaymentStatus? status = null)
        {
            try
            {
                var userId = GetUserIdFromClaims();

                if (userId == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.Forbidden,
                        Message = "Người dùng không được xác thực!"
                    };
                }

                var query = _context.Payments.Where(p => p.UserPaymentId == userId);

                if (status.HasValue)
                {
                    query = query.Where(p => p.Status == status.Value);
                }

                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                var items = await query
                    .OrderByDescending(p => p.PaymentEndDate)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var dtoItems = items.Select(p => _mapper.Map<PaymentDTO>(p)).ToList();

                var result = new PaginatedList<PaymentDTO>(dtoItems, pageIndex, totalPages, totalCount);

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Lấy danh sách thanh toán thành công!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi lấy danh sách thanh toán: " + ex.Message
                };
            }
        }

        public async Task<PaginatedList<PaymentDTO>> GetPaymentsAsync(int pageIndex, int pageSize, PaymentStatus? status = null)
        {
            var query = _context.Payments.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status.Value);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var items = await query
                .OrderByDescending(p => p.PaymentEndDate) 
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtoItems = items.Select(p => _mapper.Map<PaymentDTO>(p)).ToList();

            return new PaginatedList<PaymentDTO>(dtoItems, pageIndex, totalPages, totalCount);
        }


        public async Task<Response> CreatePaymentUrl(Guid paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "không tìm thấy thông tin khoản giao dịch!"
                };
            }

            // Lấy cấu hình VNPay
            string vnp_Returnurl = _configuration.GetValue<string>("VNPay:vnp_Returnurl")!;
            string vnp_Url = _configuration.GetValue<string>("VNPay:vnp_Url")!;
            string vnp_TmnCode = _configuration.GetValue<string>("VNPay:vnp_TmnCode")!;
            string vnp_HashSecret = _configuration.GetValue<string>("VNPay:vnp_HashSecret")!;

            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Vui lòng cấu hình các tham số: vnp_TmnCode, vnp_HashSecret trong file web.config"
                };
            }

            VnPayLibrary vnpay = new VnPayLibrary();

            // Thêm dữ liệu yêu cầu
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((double)(payment.TotalPrice * 10000)).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán hóa đơn: {paymentId}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString("N").Substring(0, 10));

            // Tạo URL
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return new Response
            {
                Code = ResponseCode.Success,
                Data = paymentUrl
            };
        }

        public async Task<Response> ProcessVnPayCallback(Guid paymentID, CreatePaymentDTO createPayment)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(paymentID);
                if (payment == null)
                {

                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Không tìm thấy danh mục cần cập nhật!"
                    };
                }
                payment.Status = PaymentStatus.Success;
                payment.PaymentCode = createPayment.PaymentCode;
                payment.PaymentMethod = createPayment.PaymentMethod;
                payment.PaymentEndDate = DateTime.Now;

                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();


                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Cập nhật thành công!",
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi cập nhật danh mục: " + ex.Message
                };
            }

        }
    }
}
