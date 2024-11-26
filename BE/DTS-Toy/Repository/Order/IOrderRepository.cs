using Business.DTO.Order;
using Business.Helper;
using Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Order
{
    public interface IOrderRepository
    {
        Task<Response> PlaceOrderAsync(Guid productId, OrderRequestDTO orderRequest);

        Task<PaginatedList<OrderDTO>> GetAllOrdersAsync(int pageIndex = 1, int pageSize = 10); // Lấy danh sách tất cả các đơn hàng
        Task<OrderDTO?> GetOrderByIdAsync(Guid orderId); // Lấy chi tiết đơn hàng theo ID

        Task<List<OrderDTO>> GetOrdersByUserIdAsync(Guid userId);//order user
    }
}
