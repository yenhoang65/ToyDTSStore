using AutoMapper;
using Business.DTO.CategoryDTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(
                    dest => dest.CategoryParentName,
                    opt => opt.MapFrom((src, dest, destMember, context) =>
                    {
                        // Kiểm tra và lấy danh mục cha từ context.Items
                        if (context.Items.TryGetValue("Categories", out var categoriesObj) && categoriesObj is IEnumerable<Category> categories)
                        {
                            var parentCategory = categories.FirstOrDefault(c => c.ID == src.ParentID);
                            return parentCategory?.CategoryName;
                        }
                        return null; // Trường hợp không tìm thấy
                    }))
                .ReverseMap();

            CreateMap<CreateUpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
