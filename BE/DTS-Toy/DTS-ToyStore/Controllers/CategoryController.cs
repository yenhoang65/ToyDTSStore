using AutoMapper;
using Business.DTO.CategoryDTO;
using Business.DTO.ContenPage;
using Business.DTO.Product;
using Business.Helper;
using Business.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.CategoryRepository;
using Repository.ContentRepository;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<PaginatedList<CategoryDTO>> GetAllCategory(int pageIndex = 1, int pageSize = 10)
        {
           return await _categoryRepository.GetAllCategory(pageIndex,pageSize);
        }

        [HttpPost]
        public async Task<Response> AddCategory([FromForm] CreateUpdateCategoryDTO categoryDTO)
        {

                return await _categoryRepository.AddCategory(categoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateCategory(Guid id, [FromForm] CreateUpdateCategoryDTO categoryDTO)
        {
                return await _categoryRepository.UpdateCategory(id, categoryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteCategory(Guid id)
        {

                return await _categoryRepository.DeleteCategory(id);

        }



    }
}
