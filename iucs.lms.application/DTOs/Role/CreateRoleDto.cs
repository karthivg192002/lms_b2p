using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.lms.application.DTOs.Permission;

namespace iucs.lms.application.DTOs.Role
{
    public class CreateRoleDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<RoleMenuPermissionDto> Permissions { get; set; } = default!;
    }
}
