using Business.Response;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Business.DTO.Users;
using System.Reflection;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Repository.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(ECommerceDBContext dbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<Response> RegisterAsync(UserRegisterDTO registerDto)
        //{
        //    try
        //    {
        //        // Kiểm tra nếu email đã tồn tại
        //        if (await _dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
        //        {
        //            return new Response { Code = ResponseCode.BadRequest, Message = "Email đã được sử dụng." };
        //        }

        //        // Lấy Permission mặc định
        //        var defaultPermission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.PermissionName == "User");
        //        if (defaultPermission == null)
        //        {
        //            return new Response { Code = ResponseCode.InternalServerError, Message = "Permission mặc định không tồn tại." };
        //        }

        //        // Lấy tất cả RoleDetail liên quan
        //        var roleDetails = await _dbContext.RolePermissions
        //            .Where(rp => rp.PermissionID == defaultPermission.ID && rp.IsEnable)
        //            .Select(rp => rp.RoleDetail)
        //            .ToListAsync();

        //        if (!roleDetails.Any())
        //        {
        //            return new Response { Code = ResponseCode.InternalServerError, Message = "Không có RoleDetails nào được gán cho Permission 'User'." };
        //        }

        //        // Tạo user
        //        var user = new User
        //        {
        //            ID = Guid.NewGuid(),
        //            FullName = registerDto.FullName,
        //            Email = registerDto.Email,
        //            PhoneNumber = registerDto.PhoneNumber,
        //            PasswordHash = HashPassword(registerDto.Password),
        //            Status = "Active",
        //            RoleID = defaultPermission.ID
        //        };

        //        _dbContext.Users.Add(user);

        //        // Gán RolePermission (Kiểm tra trước khi thêm)
        //        foreach (var roleDetail in roleDetails)
        //        {
        //            var existingRolePermission = await _dbContext.RolePermissions
        //                .FirstOrDefaultAsync(rp => rp.PermissionID == defaultPermission.ID && rp.RoleDetailID == roleDetail.ID);

        //            if (existingRolePermission == null)
        //            {
        //                _dbContext.RolePermissions.Add(new RolePermission
        //                {
        //                    PermissionID = defaultPermission.ID,
        //                    RoleDetailID = roleDetail.ID,
        //                    IsEnable = true
        //                });
        //            }
        //        }

        //        // Lưu thay đổi
        //        await _dbContext.SaveChangesAsync();

        //        return new Response
        //        {
        //            Code = ResponseCode.Success,
        //            Message = "Đăng ký thành công!",
        //            Data = new
        //            {
        //                user.ID,
        //                user.FullName,
        //                user.Email,
        //                PermissionName = defaultPermission.PermissionName,
        //                RoleDetails = roleDetails.Select(rd => rd.RoleDetailName).ToList()
        //            }
        //        };
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return new Response
        //        {
        //            Code = ResponseCode.InternalServerError,
        //            Message = $"Đã xảy ra lỗi khi đăng ký: {ex.InnerException?.Message ?? ex.Message}"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            Code = ResponseCode.InternalServerError,
        //            Message = $"Đã xảy ra lỗi không xác định: {ex.Message}"
        //        };
        //    }
        //}
        public async Task<Response> RegisterAsync(UserRegisterDTO registerDto)
        {
            try
            {
                // Kiểm tra nếu email đã tồn tại
                if (await _dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Email đã được sử dụng." };
                }

                // Tìm Permission mặc định "User"
                var defaultPermission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.PermissionName == "User");
                if (defaultPermission == null)
                {
                    return new Response { Code = ResponseCode.InternalServerError, Message = "Permission mặc định không tồn tại." };
                }

                // Tạo user mới
                var user = new User
                {
                    ID = Guid.NewGuid(),
                    Email = registerDto.Email,
                    PasswordHash = HashPassword(registerDto.Password), // Hash mật khẩu
                    Status = "Active",
                    RoleID = defaultPermission.ID
                };

                _dbContext.Users.Add(user);

                // Lưu thay đổi vào database
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Đăng ký thành công!",
                    Data = new
                    {
                        user.ID,
                        user.Email,
                        PermissionName = defaultPermission.PermissionName
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = $"Đã xảy ra lỗi khi đăng ký: {ex.InnerException?.Message ?? ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = $"Đã xảy ra lỗi không xác định: {ex.Message}"
                };
            }
        }


        public async Task<bool> CheckPermissionAsync(Guid userId, string requiredPermission)
        {
            var user = await _dbContext.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .FirstOrDefaultAsync(u => u.ID == userId);

            if (user == null) return false;

            var hasPermission = user.Role.RolePermissions.Any(rp =>
                rp.RoleDetail.RoleDetailName == requiredPermission && rp.IsEnable);

            return hasPermission;
        }



        public async Task<Response> LoginAsync(UserLoginDto loginDto)
        {
            // Lấy thông tin người dùng từ cơ sở dữ liệu
            var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Email hoặc mật khẩu không đúng!" };
            }

            // Xác minh mật khẩu
            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Email hoặc mật khẩu không đúng!" };
            }
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.PermissionName)
            };
            // Kiểm tra token hiện tại
            if (user.AccessToken == "InvalidToken1970" ||
                user.AccessTokenCreated == null ||
                user.AccessTokenCreated.Value.AddMinutes(20) <= DateTime.UtcNow)
            {
                // Nếu token hết hạn hoặc không hợp lệ, tạo token mới
                DateTime accessTokenExpiration;
                string accessToken = GenerateJwtToken(user, out accessTokenExpiration);
                string refreshToken = GenerateRefreshToken();

                user.AccessToken = accessToken;
                user.AccessTokenCreated = DateTime.UtcNow;
                user.RefreshToken = refreshToken;
                user.RefreshTokenCreated = DateTime.UtcNow;
                user.RefreshTokenExpires = DateTime.UtcNow.AddMinutes(20);

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                // Trả về token mới
                var tokenResponse = new
                {
                    UserID = user.ID,
                    AccessToken = accessToken,
                    AccessTokenExpires = accessTokenExpiration,
                    RefreshToken = refreshToken,
                    RefreshTokenExpires = user.RefreshTokenExpires
                };

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Đăng nhập thành công!",
                    Data = tokenResponse
                };
            }

            // Nếu token vẫn còn hợp lệ
            var existingTokenResponse = new
            {
                AccessToken = user.AccessToken,
                AccessTokenExpires = user.AccessTokenCreated.Value.AddMinutes(20),
                RefreshToken = user.RefreshToken,
                RefreshTokenExpires = user.RefreshTokenExpires
            };

            return new Response
            {
                Code = ResponseCode.Success,
                Message = "Token vẫn hợp lệ.",
                Data = existingTokenResponse
            };
        }


        private string GenerateJwtToken(User user, out DateTime accessTokenExpiration)
        {
            accessTokenExpiration = DateTime.UtcNow.AddMinutes(20); // Token 20 

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role?.PermissionName ?? "User")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: accessTokenExpiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToHexString(hash);
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var enteredHash = HashPassword(enteredPassword);
            return storedHash == enteredHash;
        }




        public async Task SendOtpToEmail(string email, string otp)
        {
            // Lấy thông tin cấu hình từ appsettings
            var smtpHost = _configuration["Smtp:Host"];
            var smtpPort = int.Parse(_configuration["Smtp:Port"]);
            var smtpEmail = _configuration["Smtp:Email"];
            var smtpPassword = _configuration["Smtp:Password"];

            var smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpEmail),
                Subject = "Mã xác thực (OTP)",
                Body = $"<p>Mã OTP của bạn là: <strong>{otp}</strong></p><p>Mã sẽ hết hạn trong vòng <strong>5 phút</strong>.</p>",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }


        public async Task<Response> RequestOtp([FromBody] string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Email không tồn tại trong hệ thống." };
            }

            // Tạo mã OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Lưu OTP vào Session
            _httpContextAccessor.HttpContext.Session.SetString("Otp", otp);
            _httpContextAccessor.HttpContext.Session.SetString("EmailForOtp", email);
            _httpContextAccessor.HttpContext.Session.SetString("OtpCreatedTime", DateTime.UtcNow.ToString("o"));

            // Gửi OTP qua email đã cấu hình
            try
            {
                await SendOtpToEmail(email, otp);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Không thể gửi email OTP. Vui lòng thử lại sau."
                };
            }

            return new Response
            {
                Code = ResponseCode.Success,
                Message = "OTP đã được gửi về email của bạn."
            };
        }
        private bool IsOtpExpired(DateTime otpCreatedTime)
        {
            return (DateTime.UtcNow - otpCreatedTime).TotalMinutes > 5;
        }

        public Response VerifyOtp(string otp)
        {
            var sessionOtp = _httpContextAccessor.HttpContext.Session.GetString("Otp");
            var sessionEmail = _httpContextAccessor.HttpContext.Session.GetString("EmailForOtp");
            var otpCreatedTimeStr = _httpContextAccessor.HttpContext.Session.GetString("OtpCreatedTime");

            if (string.IsNullOrEmpty(sessionOtp) || string.IsNullOrEmpty(sessionEmail) || string.IsNullOrEmpty(otpCreatedTimeStr))
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "OTP đã hết hạn hoặc không hợp lệ."
                };
            }

            // Kiểm tra thời gian OTP hết hạn
            var otpCreatedTime = DateTime.Parse(otpCreatedTimeStr);
            if (IsOtpExpired(otpCreatedTime))
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "OTP đã hết hạn. Vui lòng yêu cầu lại OTP mới."
                };
            }

            // Kiểm tra OTP có khớp không
            if (sessionOtp != otp)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "OTP không chính xác."
                };
            }

            return new Response { Code = ResponseCode.Success, Message = "OTP hợp lệ." };
        }



        public async Task<Response> ResetPassword(ResetPasswordRequestDTO request)
        {
            var sessionEmail = _httpContextAccessor.HttpContext.Session.GetString("EmailForOtp");
            if (string.IsNullOrEmpty(sessionEmail))
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Không có email hợp lệ trong phiên làm việc." };
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == sessionEmail);
            if (user == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Người dùng không tồn tại." };
            }

            user.PasswordHash = HashPassword(request.NewPassword);
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            // Xóa OTP khỏi session sau khi đặt lại mật khẩu thành công
            _httpContextAccessor.HttpContext.Session.Remove("Otp");
            _httpContextAccessor.HttpContext.Session.Remove("EmailForOtp");

            return new Response { Code = ResponseCode.Success, Message = "Đặt lại mật khẩu thành công." };
        }

        public async Task<Response> UpdateProfileAsync(Guid userId, UpdateProfileDTO profileDto)
        {
            // Tìm người dùng theo ID
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Người dùng không tồn tại."
                };
            }

            // Cập nhật các trường từ DTO
            if (!string.IsNullOrEmpty(profileDto.FullName))
                user.FullName = profileDto.FullName;

            if (!string.IsNullOrEmpty(profileDto.PhoneNumber))
                user.PhoneNumber = profileDto.PhoneNumber;

            if (!string.IsNullOrEmpty(profileDto.Gender))
                user.Gender = profileDto.Gender;

            user.BOD = (profileDto.BOD ?? DateTime.UtcNow).Date;

            // Xử lý AvatarPath (nếu có tệp tải lên)
            if (profileDto.AvatarPath != null)
            {
                var folderPath = Path.Combine("Uploads", "User");
                var avatarFilePath = Path.Combine(folderPath, profileDto.AvatarPath.FileName);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                try
                {
                    // Lưu file lên server
                    using (var stream = new FileStream(avatarFilePath, FileMode.Create))
                    {
                        await profileDto.AvatarPath.CopyToAsync(stream);
                    }

                    user.AvatarPath = avatarFilePath;
                }
                catch (Exception)
                {
                    // Nếu lưu file thất bại, giữ nguyên AvatarPath cũ
                    user.AvatarPath = null;
                }
            }

            // Xử lý AvatarName (sử dụng làm fallback nếu AvatarPath không hợp lệ)
            if (!string.IsNullOrEmpty(profileDto.AvatarName))
            {
                user.AvatarName = profileDto.AvatarName; // Lưu 
            }

            try
            {
                // Lưu thay đổi vào database
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Dữ liệu đã được cập nhật.",
                    Data = new
                    {
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        Gender = user.Gender,
                        BOD = user.BOD?.ToString("yyyy-MM-dd"),
                        AvatarPath = user.AvatarPath ?? user.AvatarName,
                        AvatarName = user.AvatarName
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Đã xảy ra lỗi khi cập nhật hồ sơ: " + ex.Message
                };
            }
        }


        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            return await _dbContext.Users
                .Select(user => new UserDTO
                {
                    ID = user.ID,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    BOD = user.BOD.HasValue ? user.BOD.Value.ToString("yyyy-MM-dd") : null,
                    AvatarPath = user.AvatarPath,
                    AvatarName = user.AvatarName
                })
                .ToListAsync();
        }



        public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
{
    return await _dbContext.Users
        .Where(u => u.ID == userId)
        .Select(user => new UserDTO
        {
            ID = user.ID,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            BOD = user.BOD.HasValue ? user.BOD.Value.ToString("yyyy-MM-dd") : null,
            AvatarPath = user.AvatarPath,
            AvatarName = user.AvatarName
        })
        .FirstOrDefaultAsync();
}


        public async Task<Response> UpdatePasswordAsync(Guid userId, string oldPassword, string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Mật khẩu mới và xác nhận mật khẩu không khớp."
                };
            }

            // Tìm người dùng theo ID
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Người dùng không tồn tại."
                };
            }

            // Kiểm tra mật khẩu cũ
            var hashedOldPassword = HashPassword(oldPassword); // Hash mật khẩu cũ
            if (user.PasswordHash != hashedOldPassword)
            {
                return new Response
                {
                    Code = ResponseCode.BadRequest,
                    Message = "Mật khẩu cũ không chính xác."
                };
            }

            // Hash mật khẩu mới và cập nhật vào database
            user.PasswordHash = HashPassword(newPassword);

            try
            {
                // Lưu thay đổi
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Cập nhật mật khẩu thành công."
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Đã xảy ra lỗi khi cập nhật mật khẩu: " + ex.Message
                };
            }
        }

        public async Task<Response> UpdateUserPermissionAsync(Guid adminId, UpdateUserPermissionDTO updatePermissionDto)
        {
            try
            {
                // Lấy thông tin admin
                var adminUser = await _dbContext.Users.Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.ID == adminId);

                if (adminUser == null || adminUser.Role?.PermissionName != "Admin")
                {
                    return new Response
                    {
                        Code = ResponseCode.Forbidden,
                        Message = "Chỉ Admin mới có quyền thay đổi quyền của người dùng khác."
                    };
                }

                // Lấy thông tin người dùng cần cập nhật
                var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.ID == updatePermissionDto.UserId);
                if (user == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Người dùng không tồn tại."
                    };
                }

                // Kiểm tra Permission ID mới có tồn tại hay không
                var newPermission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.ID == updatePermissionDto.NewPermissionId);
                if (newPermission == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Permission không tồn tại."
                    };
                }

                // Cập nhật Permission ID cho user
                user.RoleID = updatePermissionDto.NewPermissionId;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Cập nhật quyền người dùng thành công.",
                    Data = new
                    {
                        UserId = user.ID,
                        NewPermissionName = newPermission.PermissionName
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = "Đã xảy ra lỗi: " + ex.Message
                };
            }
        }


        public async Task<Response> AddRoleDetailToPermissionAsync(AddRoleDetailDTO dto)
        {
            var permission = await _dbContext.Permissions
                .FirstOrDefaultAsync(p => p.PermissionName == dto.PermissionName);

            if (permission == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Permission không tồn tại."
                };
            }

            var roleDetail = await _dbContext.RoleDetails
                .FirstOrDefaultAsync(rd => rd.RoleDetailName == dto.RoleDetailName);

            if (roleDetail == null)
            {
                roleDetail = new Data.Models.RoleDetail
                {
                    ID = Guid.NewGuid(),
                    RoleDetailName = dto.RoleDetailName
                };

                await _dbContext.RoleDetails.AddAsync(roleDetail);
            }

            var rolePermission = new RolePermission
            {
                PermissionID = permission.ID,
                RoleDetailID = roleDetail.ID,
                IsEnable = dto.IsEnable
            };

            _dbContext.RolePermissions.Add(rolePermission);
            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Code = ResponseCode.Success,
                Message = "Thêm RoleDetail vào Permission thành công.",
                Data = new { RoleDetail = roleDetail, RolePermission = rolePermission }
            };
        }

        public async Task<Response> UpdateRoleDetailNameAsync(UpdateRoleDetailDTO dto)
        {
            try
            {
                // Tìm Permission theo tên
                var permission = await _dbContext.Permissions
                    .FirstOrDefaultAsync(p => p.PermissionName == dto.PermissionName);

                if (permission == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "Permission không tồn tại."
                    };
                }

                // Tìm RoleDetail hiện tại
                var currentRoleDetail = await _dbContext.RoleDetails
                    .FirstOrDefaultAsync(rd => rd.RoleDetailName == dto.CurrentRoleDetailName);

                if (currentRoleDetail == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "RoleDetail hiện tại không tồn tại."
                    };
                }

                // Tìm RolePermission với RoleDetail hiện tại
                var rolePermission = await _dbContext.RolePermissions
                    .FirstOrDefaultAsync(rp => rp.PermissionID == permission.ID && rp.RoleDetailID == currentRoleDetail.ID);

                if (rolePermission == null)
                {
                    return new Response
                    {
                        Code = ResponseCode.NotFound,
                        Message = "RolePermission không tồn tại cho RoleDetail hiện tại."
                    };
                }

                // Tìm hoặc tạo RoleDetail mới
                var newRoleDetail = await _dbContext.RoleDetails
                    .FirstOrDefaultAsync(rd => rd.RoleDetailName == dto.NewRoleDetailName);

                if (newRoleDetail == null)
                {
                    newRoleDetail = new Data.Models.RoleDetail
                    {
                        ID = Guid.NewGuid(),
                        RoleDetailName = dto.NewRoleDetailName
                    };

                    await _dbContext.RoleDetails.AddAsync(newRoleDetail);
                    await _dbContext.SaveChangesAsync(); // Lưu trước khi tạo RolePermission mới
                }

                // Xóa RolePermission hiện tại
                _dbContext.RolePermissions.Remove(rolePermission);
                await _dbContext.SaveChangesAsync();

                // Tạo RolePermission mới với RoleDetail mới
                var newRolePermission = new RolePermission
                {
                    PermissionID = permission.ID,
                    RoleDetailID = newRoleDetail.ID,
                    IsEnable = dto.IsEnable
                };

                await _dbContext.RolePermissions.AddAsync(newRolePermission);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Code = ResponseCode.Success,
                    Message = "Cập nhật RoleDetail thành công.",
                    Data = new
                    {
                        Permission = permission.PermissionName,
                        NewRoleDetail = newRoleDetail.RoleDetailName,
                        IsEnable = newRolePermission.IsEnable
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = ResponseCode.InternalServerError,
                    Message = $"Đã xảy ra lỗi: {ex.Message}"
                };
            }
        }


        public async Task<Response> DeleteRoleDetailPermissionAsync(string permissionName, string roleDetailName)
        {
            var permission = await _dbContext.Permissions
                .FirstOrDefaultAsync(p => p.PermissionName == permissionName);

            if (permission == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Permission không tồn tại."
                };
            }

            var roleDetail = await _dbContext.RoleDetails
                .FirstOrDefaultAsync(rd => rd.RoleDetailName == roleDetailName);

            if (roleDetail == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "RoleDetail không tồn tại."
                };
            }

            var rolePermission = await _dbContext.RolePermissions
                .FirstOrDefaultAsync(rp => rp.PermissionID == permission.ID && rp.RoleDetailID == roleDetail.ID);

            if (rolePermission == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "RolePermission không tồn tại."
                };
            }

            _dbContext.RolePermissions.Remove(rolePermission);
            await _dbContext.SaveChangesAsync();

            return new Response
            {
                Code = ResponseCode.Success,
                Message = "Xóa RoleDetail khỏi Permission thành công."
            };
        }

        public async Task<Response> GetRoleDetailsByPermissionAsync(string permissionName)
        {
            var permission = await _dbContext.Permissions
                .FirstOrDefaultAsync(p => p.PermissionName == permissionName);

            if (permission == null)
            {
                return new Response
                {
                    Code = ResponseCode.NotFound,
                    Message = "Permission không tồn tại."
                };
            }

            var roleDetails = await _dbContext.RolePermissions
                .Where(rp => rp.PermissionID == permission.ID && rp.IsEnable)
                .Select(rp => rp.RoleDetail)
                .ToListAsync();

            return new Response
            {
                Code = ResponseCode.Success,
                Message = "Lấy danh sách RoleDetails thành công.",
                Data = roleDetails
            };
        }
    }
}
