using Business.DTO.Product;
using Business.Helper;
using Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task<PaginatedList<ProductResponseDTO>> GetAllProduct(
            int pageIndex = 1,
            int pageSize = 10);
        Task<ProductDetailsDTO> GetProductDetails(Guid productId);
        Task<Response> AddProduct(ProductCreateDTO productDto);
        Task<List<ProductWithRateResponse>> GetProductsByCategoryId(Guid categoryId);
        Task<Response> UpdateProduct(Guid id, ProductUpdateDTO productDto);

        Task<Response> DeleteProduct(Guid id);
    }
}
