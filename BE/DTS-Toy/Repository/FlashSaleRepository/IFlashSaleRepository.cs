using Business.DTO.FlashSale;
using Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.FlashSaleRepository
{
    public interface IFlashSaleRepository
    {
        Task<Response> AddFlashSale(FlashSaleCreateDTO flashSaleDto);
        Task<List<FlashSaleDTO>> GetFlashSalesAsync();
    }
}
