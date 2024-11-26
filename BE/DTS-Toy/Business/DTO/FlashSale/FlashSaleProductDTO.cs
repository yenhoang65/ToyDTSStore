﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.FlashSale
{
    public class FlashSaleProductDTO
    {
        public Guid? ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? PrevPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
