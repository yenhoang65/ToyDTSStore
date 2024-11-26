using Business.Helper;
using Business.Response;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Repository.FavoriteRepository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ECommerceDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LanguageHelper _languageHelper;

        public FavoriteRepository(ECommerceDBContext context, IHttpContextAccessor httpContextAccessor, LanguageHelper languageHelper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _languageHelper = languageHelper;
        }

        private Guid GetUserIdFromClaims()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("Không tìm thấy UserID trong Claim.");
            }

            return Guid.Parse(userIdClaim);
        }

        public async Task<Response> AddFavorite(Guid productId)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                var userId = GetUserIdFromClaims();

                var existingFavorite = await _context.Favorites
                    .FirstOrDefaultAsync(f => f.ProductID == productId && f.UserID == userId);

                if (existingFavorite != null)
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_FAVORITE_ALREADY_EXISTS", language)
                    };
                }

                var favorite = new Favorite
                {
                    ProductID = productId,
                    UserID = userId
                };

                await _context.Favorites.AddAsync(favorite);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("ADD_FAVORITE_SUCCESS", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_ADD_FAVORITE", language)
                };
            }
        }

        public async Task<Response> DeleteFavorite(Guid productId)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                var userId = GetUserIdFromClaims();

                var favorite = await _context.Favorites
                    .FirstOrDefaultAsync(f => f.ProductID == productId && f.UserID == userId);

                if (favorite == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_FAVORITE_NOT_FOUND", language)
                    };
                }

                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("DELETE_FAVORITE_SUCCESS", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_DELETE_FAVORITE", language)
                };
            }
        }

        public async Task<PaginatedList<Favorite>> GetFavorites(int pageIndex = 1, int pageSize = 10)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                var userId = GetUserIdFromClaims();

                var query = _context.Favorites
                    .Where(f => f.UserID == userId)
                    .Include(f => f.Product)
                    .AsQueryable();

                var count = await query.CountAsync();

                var favorites = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<Favorite>(favorites, count, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(await _languageHelper.GetTranslatedMessage("ERROR_GET_FAVORITES", language));
            }
        }
    }
}
