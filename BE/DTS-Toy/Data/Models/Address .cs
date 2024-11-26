using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Address
    {
        public Guid ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Note { get; set; }

        public Guid? UserID { get; set; }
        public User? User { get; set; }


    }
}
