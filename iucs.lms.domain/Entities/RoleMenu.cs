using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class RoleMenuBase : BaseEntity
{
    public Guid RoleId { get; set; }
    public Guid MenuId { get; set; }
    public bool CanCreate { get; set; }
    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
}

public class RoleMenu : RoleMenuBase
{
    public Role Role { get; set; } = null!;
    public Menu Menu { get; set; } = null!;
}
