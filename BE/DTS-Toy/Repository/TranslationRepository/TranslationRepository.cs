using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.TranslationRepository
{
    public class TranslationRepository: ITranslationRepository
    {
        private readonly ECommerceDBContext _context;

        public TranslationRepository(ECommerceDBContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllTranslationsAsync(string language)
        {
            var translations = await _context.Translations
                .Where(t => t.Language == language)
                .ToListAsync();

            return translations.ToDictionary(t => t.TranslationKey, t => t.TranslationValue);
        }
    }
}
