using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Users
{
    public class AddRoleDetailDTO
    {
        public string PermissionName { get; set; } = string.Empty; 
        public string RoleDetailName { get; set; } = string.Empty;
        public bool IsEnable { get; set; }


    }
}
