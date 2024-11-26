using Business.DTO.Rate;
using Business.Helper;
using Business.Response;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.RateRepository;
using System.Security.Claims;

public class RateRepository : IRateRepository
{
    private readonly ECommerceDBContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RateRepository(ECommerceDBContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response> AddRate(Guid productId,RateCreateDTO rateDto)
    {
        try
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return new Response
                {
                    Code = ResponseCode.Forbidden,
                    Message = "Người dùng chưa đăng nhập!"
                };
            }

            var userId = Guid.Parse(userIdClaim.Value);
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Sản phẩm không tồn tại!"
                };
            }

            var rate = new Rate
            {
                ID = Guid.NewGuid(),
                UserID = userId,
                ProductID = productId,
                RateValue = rateDto.RateValue,
                Comment = rateDto.Comment,
                Date = DateTime.Now
            };

            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();

            return new Response
            {
                Code = ResponseCode.Success,
                Message = "Thêm đánh giá thành công!"
            };
        }
        catch (Exception ex)
        {
            return new Response
            {
                Code = ResponseCode.BadRequest,
                Message = "Đã xảy ra lỗi: " + ex.Message
            };
        }
    }
    public async Task<PaginatedList<RateResponseDTO>> GetRatesByProductId(Guid productId, int pageIndex = 1, int pageSize = 10)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Product ID không hợp lệ.", nameof(productId));

        var query = _context.Rates
            .Where(r => r.ProductID == productId)
            .Select(r => new RateResponseDTO
            {
                RateValue = r.RateValue,
                Comment = r.Comment,
                Date = r.Date,
                UserName = r.User.FullName
            });

        var totalRecords = await query.CountAsync();

        var rates = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<RateResponseDTO>(rates, totalRecords, pageIndex, pageSize);
    }



}
