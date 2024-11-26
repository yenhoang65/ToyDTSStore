using Business.DTO.Brand;
using Business.Response;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Business.Helper;
using Business.DTO.Product;

namespace Repository.BrandRepository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ECommerceDBContext _dbContext;
        private readonly LanguageHelper _languageHelper;

        public BrandRepository(ECommerceDBContext dbContext, LanguageHelper languageHelper)
        {
            _dbContext = dbContext;
            _languageHelper = languageHelper;
        }

        public async Task<Response> AddBrandAsync(BrandCreateDTO brandDto)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                if (string.IsNullOrEmpty(brandDto.BrandName))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_NAME_REQUIRED", language)
                    };
                }

                if (await _dbContext.Brands.AnyAsync(b => b.BrandName == brandDto.BrandName))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_ALREADY_EXISTS", language)
                    };
                }

                var brand = new Brand
                {
                    ID = Guid.NewGuid(),
                    BrandName = brandDto.BrandName,
                    Link = brandDto.Link
                };

                _dbContext.Brands.Add(brand);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("SUCCESS_BRAND_ADDED", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_ADD_FAILED", language)
                };
            }
        }


        public async Task<Response> UpdateBrandAsync(Guid id, BrandUpdateDTO brandDto)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                if (string.IsNullOrEmpty(brandDto.BrandName))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_NAME_REQUIRED", language)
                    };
                }

                var existingBrand = await _dbContext.Brands.FindAsync(id);
                if (existingBrand == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_NOT_FOUND", language)
                    };
                }

                existingBrand.BrandName = brandDto.BrandName ?? existingBrand.BrandName;
                existingBrand.Link = brandDto.Link ?? existingBrand.Link;

                _dbContext.Brands.Update(existingBrand);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("SUCCESS_BRAND_UPDATED", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_UPDATE_FAILED", language)
                };
            }
        }


        public async Task<Response> DeleteBrandAsync(Guid id)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                var brand = await _dbContext.Brands.FindAsync(id);
                if (brand == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_NOT_FOUND", language)
                    };
                }

                _dbContext.Brands.Remove(brand);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("SUCCESS_BRAND_DELETED", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_BRAND_DELETE_FAILED", language)
                };
            }
        }

        public async Task<PaginatedList<BrandResponseDTO>> GetAllBrandsAsync(int pageIndex = 1,
            int pageSize = 10)
        {
            var query = _dbContext.Brands.AsQueryable();
            var count = await query.CountAsync();
            var brandList = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var brands= await _dbContext.Brands
                .Select(brand => new BrandResponseDTO
                {
                    ID = brand.ID,
                    BrandName = brand.BrandName,
                    Link = brand.Link
                })
                .ToListAsync();

            return new PaginatedList<BrandResponseDTO>(brands, count, pageIndex, pageSize);
        }

        public async Task<BrandResponseDTO?> GetBrandByIdAsync(Guid id)
        {
            var brand = await _dbContext.Brands
                .Where(b => b.ID == id)
                .Select(b => new BrandResponseDTO
                {
                    ID = b.ID,
                    BrandName = b.BrandName,
                    Link = b.Link
                })
                .FirstOrDefaultAsync();

            return brand;
        }
    }
}
