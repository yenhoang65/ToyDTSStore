using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Brand
{
    public class BrandCreateDTO
    {
        public string? BrandName { get; set; }
        public string? Link { get; set; }
    }
}
