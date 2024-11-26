using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Users
{
    public class UpdateRoleDetailDTO
    {
   
        public string PermissionName { get; set; } = string.Empty; 

        public string CurrentRoleDetailName { get; set; } = string.Empty; 

        public string NewRoleDetailName { get; set; } = string.Empty; 

        public bool IsEnable { get; set; } 
    }
}
