using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Users
{
    public class UpdateProfileDTO
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? BOD { get; set; }
        public IFormFile? AvatarPath { get; set; }
        public string? AvatarName { get; set; }
    }
}
