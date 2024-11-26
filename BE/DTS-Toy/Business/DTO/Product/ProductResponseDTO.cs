using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Product
{
    public class ProductResponseDTO
    {
        public Guid ID { get; set; }
        public string? ProductName { get; set; }
        public string? Desciption { get; set; }
        public string? Sku { get; set; }
        public decimal? Price { get; set; }
        public decimal? PrevPrice { get; set; }
        public decimal? Discount { get; set; }
        public int? Quantity { get; set; }
        public string? ImagePathsJson { get; set; }
        public string? ImageNamesJson { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? BrandID { get; set; }

    }
}
