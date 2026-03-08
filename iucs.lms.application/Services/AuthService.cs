using AutoMapper;
using iucs.lms.domain.Repositories;
using iucs.lms.domain.Entities;
using iucs.lms.api.DTOs.Auth;
using iucs.lms.api.DTOs.Users;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace iucs.lms.api.Services;
public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
    Task<TokenDto> RefreshTokenAsync(RefreshTokenDto dto);
    Task LogoutAsync(string refreshToken);
}
public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserSession> _sessionRepo;
    private readonly IRepository<UserDevice> _deviceRepo;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public AuthService(IRepository<User> userRepository, IRepository<UserSession> sessionRepo,
        IRepository<UserDevice> deviceRepo, IMapper mapper, IConfiguration config)
    {
        _userRepository = userRepository;
        _sessionRepo = sessionRepo;
        _deviceRepo = deviceRepo;
        _mapper = mapper;
        _config = config;
    }

    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var users = await _userRepository.FindAsync(u => u.Email == loginDto.Email || u.Username == loginDto.Email);
        var user = users.FirstOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("User account is disabled.");
        }

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        var session = new UserSession
        {
            UserId = user.Id,
            Token = refreshToken,
            DeviceBindingId = loginDto.DeviceId ?? "",
            ExpiresAt = DateTime.UtcNow.AddMinutes(30)
        };

        await _sessionRepo.AddAsync(session);
        await _sessionRepo.SaveChangesAsync();

        await TrackDevice(user.Id, loginDto);

        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            ExpiresTime = 30
        };
    }

    public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUsers = await _userRepository.FindAsync(u => u.Email == registerDto.Email);
        if (existingUsers.Any())
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        var user = _mapper.Map<User>(registerDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<TokenDto> RefreshTokenAsync(RefreshTokenDto dto)
    {
        var sessions = await _sessionRepo.FindAsync(x => x.Token == dto.RefreshToken && !x.IsRevoked);

        var session = sessions.FirstOrDefault();

        if (session == null || session.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid refresh token");

        var user = await _userRepository.GetByIdAsync(session.UserId);

        var newAccessToken = GenerateAccessToken(user!);
        var newRefreshToken = GenerateRefreshToken();

        session.Token = newRefreshToken;

        _sessionRepo.Update(session);
        await _sessionRepo.SaveChangesAsync();

        return new TokenDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30)
        };
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var sessions = await _sessionRepo.FindAsync(x => x.Token == refreshToken);

        var session = sessions.FirstOrDefault();

        if (session == null)
            return;

        session.IsRevoked = true;

        _sessionRepo.Update(session);
        await _sessionRepo.SaveChangesAsync();
    }

    private async Task TrackDevice(Guid userId, LoginDto dto)
    {
        var devices = await _deviceRepo.FindAsync(x =>
            x.DeviceId == dto.DeviceId && x.UserId == userId);

        var device = devices.FirstOrDefault();

        if (device == null)
        {
            device = new UserDevice
            {
                UserId = userId,
                DeviceId = dto.DeviceId ?? "",
                DeviceType = dto.DeviceType ?? ""
            };

            await _deviceRepo.AddAsync(device);
        }
        else
        {
            device.LastLogin = DateTime.UtcNow;
            _deviceRepo.Update(device);
        }

        await _deviceRepo.SaveChangesAsync();
    }

    private string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "a_very_long_super_secret_key_12345!"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.UserType)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
