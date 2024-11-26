using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.CategoryDTO
{

    public class CategoryDTO
    {
        public Guid ID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public Guid? ParentID { get; set; }
        public string? CategoryParentName { get; set; }


        public string? ImagePathsJson { get; set; }
        public string? ImageNamesJson { get; set; }
    }
}
