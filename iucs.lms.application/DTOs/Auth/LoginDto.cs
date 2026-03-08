namespace iucs.lms.api.DTOs.Auth;

public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? DeviceId { get; set; } = string.Empty;
    public string? DeviceType { get; set; } = string.Empty;
}
