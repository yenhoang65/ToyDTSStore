using Business.DTO.Users;
using Business.Response;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UsersRepository
{
    public interface IUserRepository
    {
        Task<Response> RegisterAsync(UserRegisterDTO registerDto);
        Task<Response> LoginAsync(UserLoginDto loginDto);


        Task<Response> RequestOtp(string email); // Gửi OTP tới email
        Response VerifyOtp(string otp); // Kiểm tra OTP
        Task<Response> ResetPassword(ResetPasswordRequestDTO request); // Đặt lại mật khẩu

        Task<Response> UpdateProfileAsync(Guid userId, UpdateProfileDTO profileDto);


        Task<List<UserDTO>> GetAllUsersAsync(); // Lấy danh sách tất cả người dùng
        Task<UserDTO?> GetUserByIdAsync(Guid userId); // Lấy thông tin chi tiết người dùng theo ID

        Task<Response> UpdatePasswordAsync(Guid userId, string oldPassword, string newPassword , string confirmNewPassword);

        Task<Response> UpdateUserPermissionAsync(Guid adminId, UpdateUserPermissionDTO updatePermissionDto);


        Task<Response> AddRoleDetailToPermissionAsync(AddRoleDetailDTO dto);
        Task<Response> UpdateRoleDetailNameAsync(UpdateRoleDetailDTO dto);
        Task<Response> DeleteRoleDetailPermissionAsync(string permissionName, string roleDetailName);
        Task<Response> GetRoleDetailsByPermissionAsync(string permissionName);
    }
}
