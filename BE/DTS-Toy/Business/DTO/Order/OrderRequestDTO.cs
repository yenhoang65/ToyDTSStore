using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Order
{
    public class OrderRequestDTO
    {
        public List<OrderDetailRequestDTO> OrderDetails { get; set; } = new List<OrderDetailRequestDTO>();

    }
}
