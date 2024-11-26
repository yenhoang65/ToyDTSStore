using AutoMapper;
using Business.DTO.Product;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductDetailsDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.BrandInfo, opt => opt.MapFrom(src =>
                src.Brand != null
                    ? $"<a href='{src.Brand.Link}' target='_blank'>{src.Brand.BrandName}</a>"
                    : "No Brand Info"))
            .ForMember(dest => dest.RateCount, opt => opt.MapFrom(src => src.Rates.Count))
            .ForMember(dest => dest.RateAverage, opt => opt.MapFrom(src => src.Rates.Any() ? (decimal?)src.Rates.Average(r => r.RateValue) : null))
            .ReverseMap();
        }
    }
}
