using AutoMapper;
using Business.DTO.Payment;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentDTO, Payment>().ReverseMap();
            CreateMap<CreatePaymentDTO, Payment>().ReverseMap();
        }
    }
}
