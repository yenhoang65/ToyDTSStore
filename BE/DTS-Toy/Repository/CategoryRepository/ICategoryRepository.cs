using Business.DTO.CategoryDTO;
using Business.DTO.Product;
using Business.Helper;
using Business.Response;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<PaginatedList<CategoryDTO>> GetAllCategory(int pageIndex = 1, int pageSize = 10);
        Task<Response> AddCategory(CreateUpdateCategoryDTO categoryDTO);
        Task<Response> UpdateCategory(Guid id, CreateUpdateCategoryDTO categoryDTO);
        Task<Response> DeleteCategory(Guid id);

    }
}
