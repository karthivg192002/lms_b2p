using iucs.lms.api.DTOs.Auth;
using iucs.lms.api.DTOs.Users;

namespace iucs.lms.api.Services;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
}
