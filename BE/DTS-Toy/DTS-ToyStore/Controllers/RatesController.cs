using Business.DTO.Rate;
using Business.Helper;
using Business.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.RateRepository;


namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateRepository _rateRepository;

        public RatesController(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        // GET: api/<RatesController>
        [HttpGet("products/{productId}/rates")]
        public async Task<PaginatedList<RateResponseDTO>> GetRatesByProductId(Guid productId, int pageIndex = 1, int pageSize = 10)
        {
            return await _rateRepository.GetRatesByProductId(productId);
        }

        [HttpPost("products/{productId}/rates")]
        public async Task<Response> AddRate(Guid productId, [FromBody] RateCreateDTO rateDto)
        {
            // Kiểm tra nếu ProductId trong URL không khớp với RateDto.ProductID
           

            return await _rateRepository.AddRate(productId,rateDto);

         
        }


    }
}
