using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.lms.application.Helper.Enums;

namespace iucs.lms.application.Helper
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : Attribute
    {
        public string MenuUrl { get; }
        public PermissionType Permission { get; }
        public PermissionAttribute(string menuUrl, PermissionType permission)
        {
            MenuUrl = menuUrl;
            Permission = permission;
        }
    }
}
