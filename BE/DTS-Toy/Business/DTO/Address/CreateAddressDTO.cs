using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Address
{
    public class CreateAddressDTO
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Note { get; set; }

    }
}
