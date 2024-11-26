using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.TranslationRepository
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        // Khởi tạo với IConfiguration để đọc cấu hình từ appsettings.json
        public LanguageMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, ITranslationRepository translationRepository)
        {
            var language = context.Request.Cookies["Language"];

            if (string.IsNullOrEmpty(language))
            {
                language = _configuration["LanguageSettings:DefaultLanguage"];
            }

            language = language == "en" || language == "vi" ? language : "vi";

            context.Items["Language"] = language;

            var translations = await translationRepository.GetAllTranslationsAsync(language);
            context.Items["Translations"] = translations;

            await _next(context);
        }

    }
}
