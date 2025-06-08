using Microsoft.AspNetCore.Mvc;
using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticate user and return JWT token.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            return response != null ? Ok(response) : Unauthorized(new { message = "Invalid credentials" });
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, statusCode: 500);
        }
    }

    /// <summary>
    /// Register a new user and send email verification.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _authService.RegisterAsync(request);
            return result ? Ok(new { message = "User registered. Check your email for verification." })
                          : BadRequest(new { message = "Registration failed" });
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, statusCode: 500);
        }
    }

    /// <summary>
    /// Refresh JWT access token using refresh token.
    /// </summary>
    //[HttpPost("refresh")]
    //public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    //{
    //    if (!ModelState.IsValid) return BadRequest(ModelState);

    //    try
    //    {
    //        var response = await _authService.RefreshTokenAsync(request);
    //        return response != null ? Ok(response) : Unauthorized(new { message = "Invalid refresh token" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return Problem(ex.Message, statusCode: 500);
    //    }
    //}

    /// <summary>
    /// Logout user and invalidate refresh token.
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {

        try
        {
            await _authService.LogoutAsync();
            return Ok(new { message = "Logged out successfully" });
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, statusCode: 500);
        }
    }

    /// <summary>
    /// Verify user's email.
    /// </summary>
    //[HttpPost("verify-email")]
    //public async Task<IActionResult> VerifyEmail([FromBody] EmailVerificationRequest request)
    //{
    //    if (!ModelState.IsValid) return BadRequest(ModelState);

    //    try
    //    {
    //        var result = await _authService.VerifyEmailAsync(request.Token);
    //        return result ? Ok(new { message = "Email verified successfully" })
    //                      : BadRequest(new { message = "Invalid or expired token" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return Problem(ex.Message, statusCode: 500);
    //    }
    //}

    /// <summary>
    /// Send password reset email.
    /// </summary>
    //[HttpPost("forgot-password")]
    //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    //{
    //    if (!ModelState.IsValid) return BadRequest(ModelState);

    //    try
    //    {
    //        await _authService.SendPasswordResetEmailAsync(request.Email);
    //        return Ok(new { message = "Password reset email sent." });
    //    }
    //    catch (Exception ex)
    //    {
    //        return Problem(ex.Message, statusCode: 500);
    //    }
    //}

    /// <summary>
    /// Reset password using token.
    /// </summary>
    [Authorize]
    [HttpPost("changepasswd")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            var result = await _authService.ChangePasswordAsync(request);
            return result ? Ok(new { message = "Password has been reset" })
                          : BadRequest(new { message = "Invalid Password" });
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, statusCode: 500);
        }
    }
}
