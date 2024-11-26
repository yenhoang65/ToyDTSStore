using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Order
    {
        public Guid ID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Status { get; set; }

        public Guid? UserID { get; set; }
        public User? User { get; set; }

        public ICollection<User> Users { get; set; } = null;
        public ICollection<OrderDetail> OrderDetails { get; set; } = null;
    }

}
