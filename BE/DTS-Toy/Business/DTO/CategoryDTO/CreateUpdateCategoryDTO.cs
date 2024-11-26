using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.CategoryDTO
{
    public class CreateUpdateCategoryDTO
    {
        public string? CategoryName { get; set; }
        public string? CategoryNameEN { get; set; }
        public string? Description { get; set; }
        public Guid? ParentID { get; set; }

        public string ImageNames { get; set; } = string.Empty;
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
