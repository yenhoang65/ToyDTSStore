using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Users
{
    public class ResetPasswordRequestDTO
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; } 
    }
}
