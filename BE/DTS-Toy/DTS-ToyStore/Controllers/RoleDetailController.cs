using Business.DTO.RoleDetail;
using Business.Helper;
using Business.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.RoleDetail;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleDetailController : ControllerBase
    {
        private readonly IRoleDetailRepository _roleDetailRepository;

        public RoleDetailController(IRoleDetailRepository roleDetailRepository)
        {
            _roleDetailRepository = roleDetailRepository;
        }

        [HttpGet]
        public async Task<PaginatedList<RoleDetailResponseDTO>> GetAllRoleDetailsAsync(int pageIndex = 1, int pageSize = 10)
        {
            return await _roleDetailRepository.GetAllRoleDetailsAsync(pageIndex, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleDetailById(Guid id)
        {
            var roleDetail = await _roleDetailRepository.GetRoleDetailByIdAsync(id);
            if (roleDetail == null)
            {
                return NotFound(new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Không tìm thấy chi tiết vai trò",
                    Data = null
                });
            }
            return Ok(new Response
            {
                Code = ResponseCode.Success,
                Message = "Lấy chi tiết vai trò thành công",
                Data = roleDetail
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleDetail([FromBody] RoleDetailAddOrUpdateDTO roleDetailDto)
        {
            var roleDetail = await _roleDetailRepository.AddRoleDetailAsync(roleDetailDto);
            if (roleDetail != null)
            {
                return Ok(new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Thêm chi tiết vai trò thành công",
                    Data = roleDetail
                });
            }
            return BadRequest(new Response
            {
                Code = ResponseCode.BadRequest,
                Message = "Thêm chi tiết vai trò thất bại",
                Data = null
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleDetail(Guid id, [FromBody] RoleDetailAddOrUpdateDTO roleDetailDto)
        {
            var roleDetail = await _roleDetailRepository.UpdateRoleDetailAsync(id, roleDetailDto);
            if (roleDetail != null)
            {
                return Ok(new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Cập nhật chi tiết vai trò thành công",
                    Data = roleDetail
                });
            }
            return NotFound(new Response
            {
                Code = ResponseCode.NotFound,
                Message = "Không tìm thấy chi tiết vai trò",
                Data = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleDetail(Guid id)
        {
            var isSuccess = await _roleDetailRepository.DeleteRoleDetailAsync(id);
            if (isSuccess)
            {
                return Ok(new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Xóa chi tiết vai trò thành công",
                    Data = null
                });
            }
            return NotFound(new Response
            {
                Code = ResponseCode.NotFound,
                Message = "Không tìm thấy chi tiết vai trò",
                Data = null
            });
        }
    }
}
