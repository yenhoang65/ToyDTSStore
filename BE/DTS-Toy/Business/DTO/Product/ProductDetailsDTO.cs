namespace Business.DTO.Product
{
    public class ProductDetailsDTO
    {
        public string? ProductName { get; set; }
        public string? Desciption { get; set; }
        public string? Sku { get; set; }
        public decimal? Price { get; set; }
        public decimal? PrevPrice { get; set; }
        public decimal? Discount { get; set; }
        public int? Quantity { get; set; }
        public string? ImagePathsJson { get; set; }
        public string? ImageNamesJson { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandInfo { get; set; } // Thông tin gộp giữa tên và link
        public int? RateCount { get; set; }
        public decimal? RateAverage { get; set; }
    }
}
