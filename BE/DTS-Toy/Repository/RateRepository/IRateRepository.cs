using Business.DTO.Rate;
using Business.Helper;
using Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RateRepository
{
    public interface IRateRepository
    {
        Task<Response> AddRate(Guid productId,RateCreateDTO rateDto);
        Task<PaginatedList<RateResponseDTO>> GetRatesByProductId(Guid productId, int pageIndex = 1, int pageSize = 10);
    }
}
