using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? BOD { get; set; }
        // Lưu danh sách đường dẫn ảnh
        public string? AvatarPath { get; set; }

        // Lưu danh sách tên file ảnh
        public string? AvatarName { get; set; }
        public string? PasswordHash { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenCreated { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? AccessTokenCreated { get; set; }
        public string? Status { get; set; }

        public Guid? RoleID { get; set; }
        public Permission? Role { get; set; }

        public ICollection<Address> Addresses { get; set; } = null;

        public ICollection<Rate> Rates { get; set; } = null;
        public ICollection<Favorite> Favorites { get; set; } = null;

    }


}
