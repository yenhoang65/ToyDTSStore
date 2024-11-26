using Business.Helper;
using Business.Response;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.FavoriteRepository
{
    public interface IFavoriteRepository
    {
        Task<Response> AddFavorite(Guid productId);
        Task<Response> DeleteFavorite(Guid productId);
        Task<PaginatedList<Favorite>> GetFavorites(int pageIndex = 1, int pageSize = 10);
    }
}
