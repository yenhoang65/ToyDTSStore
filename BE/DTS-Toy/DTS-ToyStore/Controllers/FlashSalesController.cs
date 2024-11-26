using Business.DTO.FlashSale;
using Business.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.FlashSaleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashSalesController : ControllerBase
    {
        private readonly IFlashSaleRepository _flashSaleRepository;

        public FlashSalesController(IFlashSaleRepository flashSaleRepository)
        {
            _flashSaleRepository = flashSaleRepository;
        }


        [HttpPost]
        public async Task<Response> AddFlashSale([FromForm] FlashSaleCreateDTO flashSaleDto)
        {
            return await _flashSaleRepository.AddFlashSale(flashSaleDto);
           
        }


        // GET: api/flashsales
        [HttpGet]
        public async Task<IActionResult> GetFlashSales()
        {
            try
            {
                var flashSales = await _flashSaleRepository.GetFlashSalesAsync();

                if (flashSales == null || !flashSales.Any())
                {
                    return NotFound(new { Message = "Không có flash sale nào đang diễn ra!" });
                }

                return Ok(flashSales);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Đã xảy ra lỗi khi lấy danh sách Flash Sale: " + ex.Message });
            }
        }
    }
}
