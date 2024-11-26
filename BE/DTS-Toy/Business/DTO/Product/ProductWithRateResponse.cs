using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Product
{
    public class ProductWithRateResponse
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string? ImagePath { get; set; }
        public decimal? Rate { get; set; }
    }
}
