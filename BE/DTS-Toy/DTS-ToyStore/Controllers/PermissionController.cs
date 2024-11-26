using Business.DTO.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.PermissionRepository;

namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        // Lấy danh sách quyền
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var response = await _permissionRepository.GetAllPermissionsAsync();
            return StatusCode((int)response.Code, response);
        }

        // Thêm quyền mới
        [HttpPost]
        public async Task<IActionResult> AddPermission([FromBody] PermissionAddDTO addPermissionDto)
        {
            var response = await _permissionRepository.AddPermissionAsync(addPermissionDto);
            return StatusCode((int)response.Code, response);
        }

        // Sửa quyền
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(Guid id, [FromBody] PermissionUpdateDTO updatePermissionDto)
        {
            var response = await _permissionRepository.UpdatePermissionAsync(id, updatePermissionDto);
            return StatusCode((int)response.Code, response);
        }

        // Xóa quyền
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(Guid id)
        {
            var response = await _permissionRepository.DeletePermissionAsync(id);
            return StatusCode((int)response.Code, response);
        }
    }
}
