﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.FlashSale
{
    public class FlashSaleDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<FlashSaleProductDTO> Products { get; set; }
    }
}
