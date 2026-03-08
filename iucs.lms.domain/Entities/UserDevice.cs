using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class UserDeviceBase : BaseEntity
{
    public int UserId { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty; // Mobile, Web, etc.
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
}

public class UserDevice : UserDeviceBase
{
    public User User { get; set; } = null!;
}
