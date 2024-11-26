using AutoMapper;
using Business.DTO;
using Business.DTO.ContenPage;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Business.Profiles
{
    public class ContentPageProfile : Profile
    {
        public ContentPageProfile()
        {
            CreateMap<ContentPage, ContentPageDTO>().ReverseMap();
            CreateMap<UpdateContentFileDTO, ContentPage>().ReverseMap();

        }
    }
}
