using Business.DTO.Product;
using Business.Helper;
using Business.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.ProductRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductResponseDTO>>> GetAllProducts(
            int pageIndex = 1,
            int pageSize = 10)
        {

            return await _productRepository.GetAllProduct(pageIndex, pageSize);
        }

        [HttpGet("byCategory/{categoryId}")]
        public async Task<ActionResult<List<ProductWithRateResponse>>> GetProductsByCategoryId(Guid categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryId(categoryId);
            return Ok(products);
        }


        [HttpGet("byProductID/{id}")]
        public async Task<IActionResult> GetProductDetails(Guid id)
        {
            try
            {
                var productDetails = await _productRepository.GetProductDetails(id);
                return Ok(productDetails);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Response>> AddProduct([FromForm] ProductCreateDTO productCreateDTO)
        {
            if (productCreateDTO == null)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Dữ liệu sản phẩm không hợp lệ."
                });
            }

            var response = await _productRepository.AddProduct(productCreateDTO);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateProduct(Guid id, [FromForm] ProductUpdateDTO productUpdateDTO)
        {
            if (productUpdateDTO == null)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Dữ liệu cập nhật không hợp lệ."
                });
            }

            var response = await _productRepository.UpdateProduct(id, productUpdateDTO);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteProduct(Guid id)
        {
            var response = await _productRepository.DeleteProduct(id);
            return Ok(response);
        }
    }
}
