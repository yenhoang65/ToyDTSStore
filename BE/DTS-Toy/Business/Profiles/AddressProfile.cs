using AutoMapper;
using Business.DTO.Address;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AddressProfile: Profile
    {
        public AddressProfile() {
            CreateMap<AddressDTO, Address>().ReverseMap();
        }
    }
}
