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

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public AuthService(IRepository<User> userRepository, IMapper mapper, IConfiguration config)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _config = config;
    }

    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var users = await _userRepository.FindAsync(u => u.Email == loginDto.Email);
        var user = users.FirstOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("User account is disabled.");
        }

        return new TokenDto
        {
            Token = GenerateJwtToken(user),
            ExpiresAt = DateTime.UtcNow.AddDays(1)
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

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "a_very_long_super_secret_key_12345!"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.UserType)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
