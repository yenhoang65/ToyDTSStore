using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Data.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public Guid? ParentID { get; set; }

        // Lưu chuỗi JSON thay vì danh sách
        public string? ImagePathsJson { get; set; }
        public string? ImageNamesJson { get; set; }

        [NotMapped]
        public List<string>? ImagePaths
        {
            get => string.IsNullOrEmpty(ImagePathsJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(ImagePathsJson);
            set => ImagePathsJson = JsonSerializer.Serialize(value);
        }
        [NotMapped]
        public List<string>? ImageNames
        {
            get => string.IsNullOrEmpty(ImageNamesJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(ImageNamesJson);
            set => ImageNamesJson = JsonSerializer.Serialize(value);
        }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    }
