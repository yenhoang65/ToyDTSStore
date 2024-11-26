using Business.DTO.Order;
using Business.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Order;
using System.Security.Claims;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder(Guid productId, [FromBody] OrderRequestDTO request)
        {
            try
            {
                var userIdClaim = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Không thể xác thực người dùng. Hãy đăng nhập lại." });
                }

                // Kiểm tra danh sách sản phẩm
                if (request.OrderDetails == null || !request.OrderDetails.Any())
                {
                    return BadRequest(new { Message = "Danh sách sản phẩm không được để trống." });
                }

                var response = await _orderRepository.PlaceOrderAsync(productId, request);

                return StatusCode((int)response.Code, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Đã xảy ra lỗi trong quá trình đặt hàng.",
                    Details = ex.Message
                });
            }
        }

        


        [HttpGet("get-all-orders")]
        [Authorize] // Chỉ người dùng đã xác thực mới có thể truy cập
        public async Task<PaginatedList<OrderDTO>> GetAllOrdersAsync(int pageIndex = 1, int pageSize = 10)
        {
            return await _orderRepository.GetAllOrdersAsync(pageIndex, pageSize);
        }


        [HttpGet("get-order/{orderId}")]
        [Authorize] // Chỉ người dùng đã xác thực mới có thể truy cập
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    return NotFound(new { Message = "Không tìm thấy đơn hàng với ID này." });
                }

                return Ok(new
                {
                    Message = "Lấy thông tin đơn hàng thành công.",
                    Data = order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Đã xảy ra lỗi khi lấy thông tin đơn hàng.",
                    Details = ex.Message
                });
            }
        }

        [HttpGet("user-orders")]
        [Authorize] 
        public async Task<IActionResult> GetOrdersForCurrentUser()
        {
            try
            {
                // Lấy UserID từ token
                var userIdClaim = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Không thể xác thực người dùng. Vui lòng đăng nhập lại." });
                }

                // Gọi repository để lấy danh sách đơn hàng của người dùng
                var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

                if (orders == null || !orders.Any())
                {
                    return NotFound(new { Message = "Không tìm thấy đơn hàng nào cho người dùng này." });
                }

                return Ok(new
                {
                    Message = "Lấy danh sách đơn hàng thành công.",
                    Data = orders
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Đã xảy ra lỗi khi lấy danh sách đơn hàng.",
                    Details = ex.Message
                });
            }
        }

    }
}
