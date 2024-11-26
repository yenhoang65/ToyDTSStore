using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class RolePermission
    {
        public Guid PermissionID { get; set; }
        public Permission? Permission { get; set; }

        public Guid? RoleDetailID { get; set; }
        public RoleDetail? RoleDetail { get; set; }

        public bool IsEnable { get; set; }
    }

}
