using Business.DTO.Address;
using Business.DTO.CategoryDTO;
using Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.AddressRepository
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressDTO>> GetAll();
        Task<IEnumerable<AddressDTO>> GetAllAddressByUserId(Guid id);
        Task<Response> AddAddress(CreateAddressDTO addressDTO);
        Task<Response> UpdateAddress(UpdateAddress addressDTO);
        Task<Response> DeleteAddress(Guid id);
    }
}
