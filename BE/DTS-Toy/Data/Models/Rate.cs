using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Rate
    {
        public Guid ID { get; set; }
        public int? RateValue { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }

        public Guid? UserID { get; set; }
        public User? User { get; set; }   
        public Guid? ProductID { get; set; }
        public Product? Product { get; set; }
    }

}
