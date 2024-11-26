using AutoMapper;
using Business.DTO.Product;
using Business.Helper;
using Business.Response;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDBContext _context;
        private readonly IMapper _mapper;
        private readonly FileUpload _fileUpload;

        public ProductRepository(ECommerceDBContext context, IMapper mapper, FileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        public async Task<PaginatedList<ProductResponseDTO>> GetAllProduct(
            int pageIndex = 1,
            int pageSize = 10)
        {
            var query = _context.Products.AsQueryable();

            var count = await query.CountAsync();

            var productList = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var productDtos = productList.Select(product => _mapper.Map<ProductResponseDTO>(product)).ToList();

            return new PaginatedList<ProductResponseDTO>(productDtos, count, pageIndex, pageSize);
        }

        public async Task<List<ProductWithRateResponse>> GetProductsByCategoryId(Guid categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryID == categoryId)
                .Select(p => new ProductWithRateResponse
                {
                    ProductId = p.ID,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ImagePath = p.ImagePaths.FirstOrDefault(),
                    Rate = Convert.ToDecimal(_context.Rates
                        .Where(r => r.ProductID == p.ID)
                        .Average(r => (double?)r.RateValue) ?? 0)
                })
                .ToListAsync();

            return products;
        }

        public async Task<ProductDetailsDTO> GetProductDetails(Guid productId)
        {
            var product = await _context.Set<Product>()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Rates) 
                .FirstOrDefaultAsync(p => p.ID == productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Sản phẩm không tồn tại.");
            }

            var productDetailsDto = _mapper.Map<ProductDetailsDTO>(product);
            return productDetailsDto;
        }


        public async Task<Response> AddProduct(ProductCreateDTO productDto)
        {
            try
            {
                var category = await _context.Categories.FindAsync(productDto.CategoryID);
                var brand = await _context.Brands.FindAsync(productDto.BrandID);

                var imageNames = productDto.ImageNames.Split(',').Select(name => name.Trim()).ToList();

                var imagePaths = _fileUpload.UploadFiles(productDto.Files, "Products", imageNames);

                decimal discountedPrice = 0;

                if (productDto.Discount > 0)
                {
                    discountedPrice = Convert.ToDecimal(productDto.PrevPrice - (productDto.PrevPrice * productDto.Discount / 100));
                }

                var product = new Product
                {
                    ID = Guid.NewGuid(),
                    ProductName = productDto.ProductName,
                    Desciption = productDto.Desciption,
                    Sku = productDto.Sku,
                    Price = discountedPrice,
                    PrevPrice = productDto.PrevPrice,
                    Discount = productDto.Discount,
                    Quantity = productDto.Quantity,
                    CategoryID = productDto.CategoryID,
                    Category = category,
                    BrandID = productDto.BrandID,
                    Brand = brand,
                    ImagePaths = imagePaths,
                    ImageNames = imageNames
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Thêm sản phẩm thành công!",
                };
            }
            catch (InvalidOperationException ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi thêm sản phẩm: " + ex.Message
                };
            }
        }



        public async Task<Response> UpdateProduct(Guid id, ProductUpdateDTO productDto)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Không tìm thấy sản phẩm cần cập nhật!"
                    };
                }

                var category = await _context.Categories.FindAsync(productDto.CategoryID);
                var brand = await _context.Brands.FindAsync(productDto.BrandID);

                if (productDto.Files != null && productDto.Files.Any())
                {
                    var imageNames = productDto.ImageNames.Split(',').Select(name => name.Trim()).ToList();

                    if (product.ImagePaths != null && product.ImagePaths.Any())
                    {
                        foreach (var path in product.ImagePaths)
                        {
                            var fullPath = Path.Combine(Directory.GetCurrentDirectory(),path);
                            if (File.Exists(fullPath))
                            {
                                File.Delete(fullPath);
                            }
                        }
                    }

                    product.ImagePaths = _fileUpload.UploadFiles(productDto.Files, "Products", imageNames);
                    product.ImageNames = imageNames;
                }
                decimal discountedPrice = 0;

                if (productDto.Discount > 0)
                {
                    discountedPrice = Convert.ToDecimal(productDto.PrevPrice - (productDto.PrevPrice * productDto.Discount / 100));
                }


                product.ProductName = productDto.ProductName;
                product.Desciption = productDto.Desciption;
                product.Sku = productDto.Sku;
                product.Price = discountedPrice;
                product.PrevPrice = productDto.PrevPrice;
                product.Discount = productDto.Discount;
                product.Quantity = productDto.Quantity;
                product.CategoryID = productDto.CategoryID;
                product.Category = category;
                product.BrandID = productDto.BrandID;
                product.Brand = brand;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Cập nhật sản phẩm thành công!",
                };
            }
            catch (InvalidOperationException ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi cập nhật sản phẩm: " + ex.Message
                };
            }
        }
        public async Task<Response> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Không tìm thấy sản phẩm cần xóa!"
                    };
                }

                var rates = _context.Rates.Where(r => r.ProductID == id).ToList();
                _context.Rates.RemoveRange(rates);
                var imagePaths = product.ImagePaths;
                foreach (var path in imagePaths)
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Xóa sản phẩm thành công!"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi xóa sản phẩm: " + ex.Message
                };
            }
        }



    }
}
