using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Translation
    {
        public Guid ID { get; set; }
        public string TranslationKey { get; set; }
        public string TranslationValue { get; set; }
        public string Language { get; set; }
    }
}
