using AutoMapper;
using Business.DTO.Address;
using Business.Helper;
using Business.Response;
using Business.Validation;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Repository.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ECommerceDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly LanguageHelper _languageHelper; // Inject LanguageHelper

        public AddressRepository(ECommerceDBContext context, IMapper mapper, IHttpContextAccessor accessor, LanguageHelper languageHelper)
        {
            _context = context;
            _mapper = mapper;
            _accessor = accessor;
            _languageHelper = languageHelper;
        }

        private Guid GetUserIdFromClaims()
        {
            var userIdClaim = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("Không tìm thấy UserID trong Claim.");
            }

            return Guid.Parse(userIdClaim);
        }

        public async Task<Response> AddAddress(CreateAddressDTO addressDTO)
        {
            var language = _languageHelper.GetCurrentLanguage(); 

            try
            {
                var userId = GetUserIdFromClaims();

                if (!ValidationHelper.IsValidLength(addressDTO.Street, 5, 200))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_STREET_LENGTH", language)
                    };
                }

                if (!ValidationHelper.IsValidLength(addressDTO.City, 2, 100))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_CITY_LENGTH", language)
                    };
                }

                if (!ValidationHelper.IsValidLength(addressDTO.Country, 2, 100))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_COUNTRY_LENGTH", language)
                    };
                }

                if (!string.IsNullOrEmpty(addressDTO.Note) && !ValidationHelper.IsValidLength(addressDTO.Note, 1, 500) || addressDTO.Note == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_NOTE_LENGTH", language)
                    };
                }

                if (userId == Guid.Empty)
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_INVALID_USERID", language)
                    };
                }

                var address = new Address
                {
                    ID = Guid.NewGuid(),
                    Street = addressDTO.Street,
                    City = addressDTO.City,
                    Country = addressDTO.Country,
                    Note = addressDTO.Note,
                    UserID = userId,
                };

                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("SUCCESS_ADDRESS_ADDED", language),
                    Data = address
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_ADD_ADDRESS", language) + ": " + ex.Message
                };
            }
        }

        public async Task<Response> DeleteAddress(Guid id)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(c => c.ID == id);
                if (address == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_NOT_FOUND", language)
                    };
                }
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("SUCCESS_ADDRESS_DELETED", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_DELETE_ADDRESS", language) + ": " + ex.Message
                };
            }
        }

        public async Task<IEnumerable<AddressDTO>> GetAll()
        {
            var allAddress = await _context.Addresses.ToListAsync();
            return allAddress.Select(c => _mapper.Map<AddressDTO>(c));
        }

        public async Task<IEnumerable<AddressDTO>> GetAllAddressByUserId(Guid id)
        {
            var allAddress = await _context.Addresses
                .Where(p => p.UserID == id)
                .ToListAsync();

            return allAddress.Select(c => _mapper.Map<AddressDTO>(c));
        }

        public async Task<Response> UpdateAddress(UpdateAddress addressDTO)
        {
            var language = _languageHelper.GetCurrentLanguage();

            try
            {
                var address = await _context.Addresses.FindAsync(addressDTO.ID);

                if (address == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_NOT_FOUND", language)
                    };
                }

                // Validate Street
                if (!ValidationHelper.IsValidLength(addressDTO.Street, 5, 200))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_STREET_LENGTH", language)
                    };
                }

                // Validate City
                if (!ValidationHelper.IsValidLength(addressDTO.City, 2, 100))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_CITY_LENGTH", language)
                    };
                }

                // Validate Country
                if (!ValidationHelper.IsValidLength(addressDTO.Country, 2, 100))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_COUNTRY_LENGTH", language)
                    };
                }

                // Validate Note (if provided)
                if (!string.IsNullOrEmpty(addressDTO.Note) &&
                    !ValidationHelper.IsValidLength(addressDTO.Note, 1, 500))
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = await _languageHelper.GetTranslatedMessage("ERROR_ADDRESS_NOTE_LENGTH", language)
                    };
                }

                address.Street = addressDTO.Street;
                address.City = addressDTO.City;
                address.Country = addressDTO.Country;
                address.Note = addressDTO.Note;

                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = await _languageHelper.GetTranslatedMessage("SUCCESS_ADDRESS_UPDATED", language)
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = await _languageHelper.GetTranslatedMessage("ERROR_UPDATE_ADDRESS", language) + ": " + ex.Message
                };
            }
        }
    }
}
