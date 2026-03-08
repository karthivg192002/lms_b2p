using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class UserRoleBase : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

public class UserRole : UserRoleBase
{
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}