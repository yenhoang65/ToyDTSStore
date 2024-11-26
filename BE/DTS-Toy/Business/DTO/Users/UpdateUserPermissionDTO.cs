using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Users
{
    public class UpdateUserPermissionDTO
    {
        public Guid UserId { get; set; } 

        public Guid NewPermissionId { get; set; } 
    }
}
