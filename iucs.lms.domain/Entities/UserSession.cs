using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class UserSessionBase : BaseEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string DeviceBindingId { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

public class UserSession : UserSessionBase
{
    public User User { get; set; } = null!;
}