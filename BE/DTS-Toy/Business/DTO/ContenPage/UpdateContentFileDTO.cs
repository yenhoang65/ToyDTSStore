using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.ContenPage
{
    public class UpdateContentFileDTO
    {
        public Guid ID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Introduction { get; set; }
        public string? Description { get; set; }
        public string? Video { get; set; }
        public string ImageNames { get; set; } = string.Empty;
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
