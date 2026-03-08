using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class RoleMenuBase : BaseEntity
{
    public int RoleId { get; set; }
    public int MenuId { get; set; }
}

public class RoleMenu : RoleMenuBase
{
    public Role Role { get; set; } = null!;
    public Menu Menu { get; set; } = null!;
}
