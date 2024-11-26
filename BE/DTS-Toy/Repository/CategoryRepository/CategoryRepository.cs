using AutoMapper;
using Business.DTO.Brand;
using Business.DTO.CategoryDTO;
using Business.DTO.ContenPage;
using Business.DTO.Product;
using Business.Helper;
using Business.Response;
using Business.Validation;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceDBContext _context;
        private readonly IMapper _mapper;
        private readonly FileUpload _fileUpload;
        private LanguageHelper _languageHelper;

        public CategoryRepository(ECommerceDBContext context, IMapper mapper, FileUpload fileUpload, LanguageHelper languageHelper)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
            _languageHelper = languageHelper;
        }

        public async Task<Response> AddCategory(CreateUpdateCategoryDTO categoryDTO)
        {
            var language = _languageHelper.GetCurrentLanguage();
            try
            {
                if (!ValidationHelper.IsValidLength(categoryDTO.CategoryName, 2, 100))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORYNAME_LENGTH", language)
                    };
                }

                if (!string.IsNullOrEmpty(categoryDTO.Description) &&
                    !ValidationHelper.IsValidLength(categoryDTO.Description, 1, 500))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORYDESCIPTION_LENGTH", language)
                    };
                }

                if (categoryDTO.Files != null && categoryDTO.Files.Any())
                {
                    if (string.IsNullOrWhiteSpace(categoryDTO.ImageNames))
                    {
                        return new Response
                        {
                            Code = ResponseCode.BadRequest,
                            Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORYIMAGENAME_NULL", language)
                        };
                    }

                    var imageNameCount = categoryDTO.ImageNames.Split(',').Length;
                    if (imageNameCount != categoryDTO.Files.Count)
                    {
                        return new Response
                        {
                            Code = ResponseCode.BadRequest,
                            Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORYIMAGENAME_LENGTH", language)
                        };
                    }
                }

                var imageNames = categoryDTO.ImageNames.Split(',').Select(name => name.Trim()).ToList();
                var imagePaths = _fileUpload.UploadFiles(categoryDTO.Files, "Category", imageNames);

                var categoryKey = "CATEGORY_" + Guid.NewGuid().ToString("N");

                var category = new Category
                {
                    ID = Guid.NewGuid(),
                    CategoryName = categoryKey,
                    Description = categoryDTO.Description,
                    ParentID = categoryDTO.ParentID,
                    ImagePaths = imagePaths,
                    ImageNames = imageNames
                };

                var translations = new List<Translation>
                {
                    new Translation
                    {
                        ID = new Guid(),
                        TranslationKey = categoryKey,
                        TranslationValue = categoryDTO.CategoryName,
                        Language = "vi"
                    },
                    new Translation
                    {
                        ID = new Guid(),
                        TranslationKey = categoryKey,
                        TranslationValue = categoryDTO.CategoryNameEN ?? categoryDTO.CategoryName,
                        Language = "en"
                    }
                };

                _context.Categories.Add(category);
                _context.Translations.AddRange(translations);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("ADD_CATEGORY_SUCCESS", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_ADD_CATEGORY", language)
                };
            }
        }

        public async Task<PaginatedList<CategoryDTO>> GetAllCategory(int pageIndex = 1, int pageSize = 10)
        {
            var language = _languageHelper.GetCurrentLanguage();

            var query = from c in _context.Categories
                        join t in _context.Translations
                        on c.CategoryName equals t.TranslationKey
                        where t.Language == language
                        select new CategoryDTO
                        {
                            ID = c.ID,
                            CategoryName = t.TranslationValue,
                            Description = c.Description,
                            ParentID = c.ParentID,
                            ImagePathsJson = c.ImagePathsJson,
                            ImageNamesJson = c.ImageNamesJson
                        };

            var count = await query.CountAsync();
            var categories = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<CategoryDTO>(categories, count, pageIndex, pageSize);
        }

        public async Task<Response> UpdateCategory(Guid id, CreateUpdateCategoryDTO categoryDTO)
        {
            var language = _languageHelper.GetCurrentLanguage();
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORY_NOT_FOUND", language)
                    };
                }

                // Validate CategoryName
                if (!ValidationHelper.IsValidLength(categoryDTO.CategoryName, 2, 100))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORYNAME_LENGTH", language)
                    };
                }

                // Validate Description
                if (!string.IsNullOrEmpty(categoryDTO.Description) &&
                    !ValidationHelper.IsValidLength(categoryDTO.Description, 1, 500))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORYDESCIPTION_LENGTH", language)
                    };
                }

                if (categoryDTO.Files != null && categoryDTO.Files.Any())
                {
                    
                    if (string.IsNullOrWhiteSpace(categoryDTO.ImageNames))
                    {
                        return new Response
                        {
                            Code = ResponseCode.BadRequest,
                            Message = await _languageHelper.GetTranslatedMessage("ERROR_IMAGE_NAME_NULL", language)
                        };
                    }

                    var imageNameCount = categoryDTO.ImageNames.Split(',').Length;
                    if (imageNameCount != categoryDTO.Files.Count)
                    {
                        return new Response
                        {
                            Code = ResponseCode.BadRequest,
                            Message = await _languageHelper.GetTranslatedMessage("ERROR_IMAGE_NAME_COUNT", language)
                        };
                    }

                    var imageNames = categoryDTO.ImageNames.Split(',').Select(name => name.Trim()).ToList();

                    // Delete old images
                    foreach (var path in category.ImagePaths)
                    {
                        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }

                    category.ImagePaths = _fileUpload.UploadFiles(categoryDTO.Files, "Category", imageNames);
                    category.ImageNames = imageNames;
                }

                category.CategoryName = categoryDTO.CategoryName;
                category.Description = categoryDTO.Description;
                category.ParentID = categoryDTO.ParentID;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("UPDATE_CATEGORY_SUCCESS", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_UPDATE_CATEGORY", language)
                };
            }
        }

        public async Task<Response> DeleteCategory(Guid id)
        {
            var language = _languageHelper.GetCurrentLanguage();
            try
            {
                var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.ID == id);

                if (category == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_CATEGORY_NOT_FOUND", language)
                    };
                }

                // Delete related products
                if (category.Products != null && category.Products.Any())
                {
                    foreach (var product in category.Products)
                    {
                        // Delete product
                        _context.Products.Remove(product);
                    }
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("DELETE_CATEGORY_SUCCESS", language)
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_DELETE_CATEGORY", language)
                };
            }
        }
    }
}
