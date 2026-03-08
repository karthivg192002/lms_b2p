namespace iucs.lms.domain.Entities;

public class UserDevice
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public string DeviceId { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty; // Mobile, Web, etc.
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
