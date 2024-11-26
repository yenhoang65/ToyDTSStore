using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Data.Models
{
    public class Product
    {
        public Guid ID { get; set; }
        public string? ProductName { get; set; }
        public string? Desciption { get; set; }
        public string? Sku { get; set; }
        public decimal? Price { get; set; }
        public decimal? PrevPrice { get; set; }
        public decimal? Discount { get; set; }
        public int? Quantity { get; set; }
        public Guid? CategoryID { get; set; }
        public Category? Category { get; set; }

        public Guid? BrandID { get; set; }
        public Brand? Brand { get; set; }

        public ICollection<Rate> Rates { get; set; } = null;
        public ICollection<Favorite> Favorites { get; set; } = null;
        public ICollection<OrderDetail> OrderDetails { get; set; } = null;
        public ICollection<FlashSale> FlashSales { get; set; } = null;


        public string? ImagePathsJson { get; set; }
        public string? ImageNamesJson { get; set; }
        [NotMapped]
        public List<string>? ImagePaths
        {
            get => string.IsNullOrEmpty(ImagePathsJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(ImagePathsJson);
            set => ImagePathsJson = JsonSerializer.Serialize(value);
        }
        [NotMapped]
        public List<string>? ImageNames
        {
            get => string.IsNullOrEmpty(ImageNamesJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(ImageNamesJson);
            set => ImageNamesJson = JsonSerializer.Serialize(value);
        }
    }
}