using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class RoleBase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class Role : RoleBase
{
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
