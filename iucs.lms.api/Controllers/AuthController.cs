using Microsoft.AspNetCore.Mvc;
using iucs.lms.api.DTOs.Auth;
using iucs.lms.api.DTOs.Users;
using iucs.lms.api.Services;
using iucs.lms.application.DTOs.Auth;

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
    #region OTP Endpoints

    [HttpPost("login/send-otp")]
    public async Task<IActionResult> SendOtp(OtpDto dto)
    {
        try
        {
            await _authService.SendOtpAsync(dto);
            return Ok("OTP sent successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("login/verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpDto dto)
    {
        try
        {
            var token = await _authService.VerifyOtpAsync(dto);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
    #endregion

    #region Forget Password Endpoints
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        try
        {
            await _authService.ForgotPasswordAsync(dto);
            return Ok("Reset link sent");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        try
        {
            await _authService.ResetPasswordAsync(dto);
            return Ok("Password updated");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        }
    }
    #endregion
}
