namespace iucs.lms.domain.Entities;

public class UserSession
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public string Token { get; set; } = string.Empty;
    public string DeviceBindingId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
}
