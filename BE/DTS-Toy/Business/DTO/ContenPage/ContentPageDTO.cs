using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.DTO.ContenPage
{
    public class ContentPageDTO
    {
        public Guid ID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Introduction { get; set; }
        public string? Description { get; set; }
        public string? Video { get; set; }
        public string? ImagePathsJson { get; set; }
        public string? ImageNamesJson { get; set; }
    }
}
