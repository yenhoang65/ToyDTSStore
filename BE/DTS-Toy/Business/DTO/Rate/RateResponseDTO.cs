using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Rate
{
    public class RateResponseDTO
    {
        public int? RateValue { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public string? UserName { get; set; }
    }

}
