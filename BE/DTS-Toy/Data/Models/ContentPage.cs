using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ContentPage
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
    }

}
