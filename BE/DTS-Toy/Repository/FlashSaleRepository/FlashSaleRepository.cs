using Business.DTO.FlashSale;
using Business.Response;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.FlashSaleRepository
{
    public class FlashSaleRepository : IFlashSaleRepository
    {
        private readonly ECommerceDBContext _context;

        public FlashSaleRepository(ECommerceDBContext context)
        {
            _context = context;
        }

        public async Task<Response> AddFlashSale(FlashSaleCreateDTO flashSaleDto)
        {
            try
            {
                var productsWithDiscount = await _context.Products
                    .Where(p => p.Discount != 0)
                    .ToListAsync();

                if (productsWithDiscount.Count == 0)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Không có sản phẩm nào có giảm giá!"
                    };
                }

                if (flashSaleDto.StartDate >= flashSaleDto.EndDate)
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Ngày bắt đầu phải trước ngày kết thúc!"
                    };
                }

                var flashSales = productsWithDiscount.Select(product => new FlashSale
                {
                    ID = Guid.NewGuid(),
                    ProductID = product.ID,
                    StartDate = flashSaleDto.StartDate,
                    EndDate = flashSaleDto.EndDate,
                    Product = product
                }).ToList();

                _context.FlashSales.AddRange(flashSales);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Thêm Flash Sale cho các sản phẩm giảm giá thành công!"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi thêm Flash Sale: " + ex.Message
                };
            }
        }

        public async Task<List<FlashSaleDTO>> GetFlashSalesAsync()
        {
            try
            {
                var flashSales = await _context.FlashSales
                    .Include(f => f.Product) 
                    .Where(f => f.Product.Discount != 0) 
                    .ToListAsync();

                var flashSaleDtos = flashSales.GroupBy(f => new { f.StartDate, f.EndDate })
                    .Select(g => new FlashSaleDTO
                    {
                        StartDate = g.Key.StartDate,
                        EndDate = g.Key.EndDate,
                        Products = g.Select(flashSale => new FlashSaleProductDTO
                        {
                            ProductID = flashSale.ProductID,
                            ProductName = flashSale.Product.ProductName,
                            Price = flashSale.Product.Price,
                            PrevPrice = flashSale.Product.PrevPrice,
                            Discount = flashSale.Product.Discount
                        }).ToList()
                    }).ToList();

                return flashSaleDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách Flash Sale: " + ex.Message);
            }
        }
    }
}
