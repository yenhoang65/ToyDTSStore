using System;
using System.ComponentModel.DataAnnotations;

namespace Business.DTO.Rate
{
    public class RateCreateDTO
    {
        public int RateValue { get; set; }

        public string? Comment { get; set; }
    }
}
