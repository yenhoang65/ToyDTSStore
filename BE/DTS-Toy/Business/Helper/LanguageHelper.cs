using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper
{
    public class LanguageHelper
    {
        public  ECommerceDBContext _context;
        public LanguageHelper(ECommerceDBContext context) 
        {
            _context= context;
        }
        public string GetCurrentLanguage()
        {
            var httpContext = new HttpContextAccessor().HttpContext;

            var language = httpContext?.Request.Cookies["Language"];

            return string.IsNullOrEmpty(language) ? "vi" : language;
        }


        public async Task<string> GetTranslatedMessage(string key, string language)
        {
            var translation = await _context.Translations
                .FirstOrDefaultAsync(t => t.TranslationKey == key && t.Language == language);
            return translation?.TranslationValue ?? key;
        }
    }
}
