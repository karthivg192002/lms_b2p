using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class UserBase : AuditEntity
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty; // Student, Teacher, Parent, Admin
    public bool EmailVerified { get; set; }
    public bool PhoneVerified { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public class User : UserBase
{
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}
