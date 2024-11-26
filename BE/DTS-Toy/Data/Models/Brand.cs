using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Brand
    {
        public Guid ID { get; set; }
        public string? BrandName { get; set; }
        public string? Link { get; set; }

        public ICollection<Product> Products { get; set; } = null;

    }

}
