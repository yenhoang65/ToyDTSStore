using Business.DTO.Users;
using Business.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.UsersRepository;
using System.Security.Claims;


namespace DTS_ToyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users); 
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "Người dùng không tồn tại." });
            }
            return Ok(user); // Trả về thông tin chi tiết người dùng
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registerDto)
        {
            var response = await _userRepository.RegisterAsync(registerDto);
            return StatusCode((int)response.Code, response);
        }

        [HttpPost("login")]
        public async Task<Response> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Dữ liệu không hợp lệ!." };
            }

            return await _userRepository.LoginAsync(loginDto);
        }

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] RequestOtpDTO requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Email))
            {
                return BadRequest(new Response { Code = ResponseCode.BadRequest, Message = "Email không được để trống." });
            }

            var response = await _userRepository.RequestOtp(requestDto.Email);
            return StatusCode((int)response.Code, response);
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpDTO otpRequest)
        {
            if (string.IsNullOrWhiteSpace(otpRequest.OTP))
            {
                return BadRequest(new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "OTP không được để trống."
                });
            }

            var response = _userRepository.VerifyOtp(otpRequest.OTP);
            return StatusCode((int)response.Code, response);
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response { Code = ResponseCode.BadRequest, Message = "Dữ liệu không hợp lệ!." });
            }

            var response = await _userRepository.ResetPassword(request);
            return StatusCode((int)response.Code, response);
        }


        [HttpPut("update-profile/{userId}")]
        public async Task<IActionResult> UpdateProfile(Guid userId, [FromForm] UpdateProfileDTO profileDto)
        {
            var response = await _userRepository.UpdateProfileAsync(userId, profileDto);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("update-password/{userId}")]
        public async Task<IActionResult> UpdatePassword(Guid userId, [FromBody] UpdatePasswordRequest request)
        {
            var response = await _userRepository.UpdatePasswordAsync(
                userId,
                request.OldPassword,
                request.NewPassword,
                request.ConfirmNewPassword
            );

            return StatusCode((int)response.Code, response);
        }

        [HttpPut("update-permission")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPermission([FromBody] UpdateUserPermissionDTO updatePermissionDto)
        {
            // Lấy UserID của admin từ token
            var adminId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Gọi repository để xử lý logic
            var response = await _userRepository.UpdateUserPermissionAsync(adminId, updatePermissionDto);

            return StatusCode((int)response.Code, response);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost("add-RolePermission")]
        public async Task<IActionResult> AddRoleDetailToPermission([FromBody] AddRoleDetailDTO dto)
        {
            var response = await _userRepository.AddRoleDetailToPermissionAsync(dto);
            return StatusCode((int)response.Code, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-RolePermission")]
        public async Task<IActionResult> UpdateRoleDetailName([FromBody] UpdateRoleDetailDTO dto)
        {
            var response = await _userRepository.UpdateRoleDetailNameAsync(dto);
            return StatusCode((int)response.Code, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-RolePermission")]
        public async Task<IActionResult> DeleteRoleDetailPermission(string permissionName, string roleDetailName)
        {
            var response = await _userRepository.DeleteRoleDetailPermissionAsync(permissionName, roleDetailName);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("list-RolePermission/{permissionName}")]
        public async Task<IActionResult> GetRoleDetailsByPermission(string permissionName)
        {
            var response = await _userRepository.GetRoleDetailsByPermissionAsync(permissionName);
            return StatusCode((int)response.Code, response);
        }
    }
}
