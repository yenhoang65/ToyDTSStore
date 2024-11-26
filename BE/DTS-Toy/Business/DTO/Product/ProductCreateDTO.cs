using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.DTO.Product
{
    public class ProductCreateDTO
    {

        public string? ProductName { get; set; }


        public string? Desciption { get; set; }

        public string? Sku { get; set; }

        public decimal? PrevPrice { get; set; }

        public decimal? Discount { get; set; }

        public int? Quantity { get; set; }

        public Guid? CategoryID { get; set; }

        public Guid? BrandID { get; set; }

        public string ImageNames { get; set; } = string.Empty;

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
