namespace iucs.lms.api.DTOs.Auth;

public class TokenDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

public class RefreshTokenDto
{
    public string RefreshToken { get; set; } = string.Empty;
}