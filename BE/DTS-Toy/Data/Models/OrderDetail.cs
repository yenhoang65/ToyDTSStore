using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderDetail
    {
        public Guid ID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }

        public Guid? ProductID { get; set; }
        public Product? Product { get; set; }

        public Guid? OrderID { get; set; }
        public Order? Order { get; set; }

    }

}
