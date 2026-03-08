using Microsoft.AspNetCore.Mvc;
using iucs.lms.api.DTOs.Auth;
using iucs.lms.api.DTOs.Users;
using iucs.lms.api.Services;

namespace iucs.lms.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(token);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var user = await _authService.RegisterAsync(registerDto);
            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenDto>> Refresh(RefreshTokenDto dto)
    {
        try
        {
            var token = await _authService.RefreshTokenAsync(dto);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenDto dto)
    {
        try
        {
            await _authService.LogoutAsync(dto.RefreshToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        }
    }
}
