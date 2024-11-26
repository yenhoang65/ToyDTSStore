using Microsoft.AspNetCore.Mvc;
using Repository.TranslationRepository;

[ApiController]
[Route("api/[controller]")]
public class TranslationController : ControllerBase
{
    private readonly ITranslationRepository _translationRepository;

    public TranslationController(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }

    [HttpGet("get-translations-by-language")]
    public async Task<IActionResult> GetTranslationsByLanguage([FromQuery] string language)
    {
        try
        {
            language = string.IsNullOrEmpty(language) ? "en" : language;
            language = language == "en" || language == "vi" ? language : "en";

            Response.Cookies.Append("Language", language, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7), 
                HttpOnly = true
            });

            var translations = await _translationRepository.GetAllTranslationsAsync(language);
            return Ok(translations);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
        }
    }

}
