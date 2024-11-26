using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.TranslationRepository
{
    public interface ITranslationRepository
    {
        Task<Dictionary<string, string>> GetAllTranslationsAsync(string language);
    }
}
