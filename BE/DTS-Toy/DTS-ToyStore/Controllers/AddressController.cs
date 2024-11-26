using Business.DTO.Address;
using Business.DTO.CategoryDTO;
using Business.DTO.ContenPage;
using Business.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.AddressRepository;
using System.Security.Claims;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAllAddress()
        {
            try
            {
                var content = await _addressRepository.GetAll();
                return Ok(new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Get all Category successfully",
                    Data = content
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = ex.Message
                });
            }
        }



        [HttpGet("GetAllAddressByUserId")]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAllAddressByUserId()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized("Không tìm thấy ID người dùng trong token");

                if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                    return BadRequest("ID người dùng không hợp lệ!");

                var content = await _addressRepository.GetAllAddressByUserId(userId);
                return Ok(new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Lấy danh sách địa chỉ thành công",
                    Data = content
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromForm] CreateAddressDTO addressDTO)
        {
            try
            {
                if (addressDTO == null)
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Dữ liệu không hợp lệ!"
                    });
                }
                var response = await _addressRepository.AddAddress(addressDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateAddress([FromForm] UpdateAddress addressDTO)
        {
            try
            {
                if (addressDTO == null)
                {
                    return BadRequest(new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "Dữ liệu không hợp lệ!"
                    });
                }
                var response = await _addressRepository.UpdateAddress(addressDTO);
                if (response.Code == ResponseCode.NotFound)
                {
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi cập nhật danh mục: " + ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteAddress(Guid id)
        {
            try
            {
                var response = await _addressRepository.DeleteAddress(id);
                if (response.Code == ResponseCode.NotFound)
                {
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Đã xảy ra lỗi khi xóa danh mục: " + ex.Message
                });
            }
        }


    }
}
