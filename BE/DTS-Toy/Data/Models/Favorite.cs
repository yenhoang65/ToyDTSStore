using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Favorite
    {
        public Guid ProductID { get; set; }
        public Product? Product { get; set; }

        public Guid? UserID { get; set; }
        public User? User { get; set; }
    }

}
