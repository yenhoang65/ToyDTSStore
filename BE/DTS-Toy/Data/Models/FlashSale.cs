using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class FlashSale
    {
        public Guid ID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid? ProductID { get; set; }
        public Product? Product { get; set; }

    }

}
