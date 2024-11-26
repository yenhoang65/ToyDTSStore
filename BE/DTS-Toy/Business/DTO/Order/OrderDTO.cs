using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Order
{
    public class OrderDTO
    {
        public Guid? OrderId { get; set; } 
        public DateTime? OrderDate { get; set; } 
        public string? Status { get; set; }
        public Guid? UserId { get; set; }
        public List<OrderDetailDTO>? OrderDetails { get; set; } 
    }
}
