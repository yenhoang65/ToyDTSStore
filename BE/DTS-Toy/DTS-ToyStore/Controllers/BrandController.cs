using Business.DTO.Brand;
using Business.Helper;
using Business.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.BrandRepository;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpPost("AddBrand")]
        public async Task<Response> AddBrand([FromBody] BrandCreateDTO brandDto)
        {
            return await _brandRepository.AddBrandAsync(brandDto);
           
        }

        [HttpPut("UpdateBrand/{id}")]
        public async Task<Response> UpdateBrand(Guid id, [FromBody] BrandUpdateDTO brandDto)
        {
            return await _brandRepository.UpdateBrandAsync(id, brandDto);
            
        }

        [HttpDelete("DeleteBrand/{id}")]
        public async Task<Response> DeleteBrand(Guid id)
        {
            return await _brandRepository.DeleteBrandAsync(id);
           
        }

        [HttpGet("GetAllBrands")]
        public async Task<PaginatedList<BrandResponseDTO>> GetAllBrandsAsync(int pageIndex = 1,
            int pageSize = 10)
        {
            return await _brandRepository.GetAllBrandsAsync(pageIndex,pageSize);
        }

        [HttpGet("GetBrandById/{id}")]
        public async Task<BrandResponseDTO> GetBrandById(Guid id)
        {
            return await _brandRepository.GetBrandByIdAsync(id);
        }
    }
}
