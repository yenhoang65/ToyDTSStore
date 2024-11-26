using Business.Helper;
using Business.Response;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.FavoriteRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoritesController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<PaginatedList<Favorite>> GetFavorites(int pageIndex = 1, int pageSize = 10)
        {
            return await _favoriteRepository.GetFavorites(pageIndex,pageSize);
        }

        [HttpPost("{productId}")]
        public async Task<Response> AddFavorite(Guid productId)
        {
                   return await _favoriteRepository.AddFavorite(productId);
        }

        [HttpDelete("{productId}")]
        public async Task<Response> DeleteFavorite(Guid productId)
        {
           
              return await _favoriteRepository.DeleteFavorite(productId);

        }
    }
}
