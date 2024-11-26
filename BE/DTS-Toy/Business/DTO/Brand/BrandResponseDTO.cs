using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Brand
{
    public class BrandResponseDTO
    {
        public Guid ID { get; set; }
        public string? BrandName { get; set; }
        public string? Link { get; set; }
    }
}
