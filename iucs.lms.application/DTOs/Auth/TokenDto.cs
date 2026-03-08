namespace iucs.lms.api.DTOs.Auth;

public class TokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
