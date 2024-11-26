﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Permission
    {
        public Guid ID { get; set; }
        public string? PermissionName { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
