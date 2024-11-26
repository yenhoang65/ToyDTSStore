using Business.DTO.Brand;
using Business.Helper;
using Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.BrandRepository
{
    public interface IBrandRepository
    {
        Task<Response> AddBrandAsync(BrandCreateDTO brandDto);
        Task<Response> UpdateBrandAsync(Guid id, BrandUpdateDTO brandDto);
        Task<Response> DeleteBrandAsync(Guid id);
        Task<PaginatedList<BrandResponseDTO>> GetAllBrandsAsync(int pageIndex = 1,
            int pageSize = 10);
        Task<BrandResponseDTO?> GetBrandByIdAsync(Guid id);
    }
}
