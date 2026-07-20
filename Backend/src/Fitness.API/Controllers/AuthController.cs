using Fitness.Application.DTOs.Auth;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [EnableRateLimiting("otp")]
    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp(
        [FromBody] SendOtpRequest request,
        CancellationToken cancellationToken)
    {
        await _authService.SendOtpAsync(
            request.PhoneNumber,
            cancellationToken);

        return Ok(new
        {
            Message = "OTP sent successfully."
        });
    }

    [EnableRateLimiting("otp")]
    [HttpPost("verify-otp")]
    public async Task<ActionResult<LoginResponse>> VerifyOtp(
        [FromBody] VerifyOtpRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _authService.VerifyOtpAsync(
            request.PhoneNumber,
            request.Code,
            cancellationToken);

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse>> Refresh(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _authService.RefreshTokenAsync(
            request.RefreshToken,
            cancellationToken);

        return Ok(response);
    }

    [HttpPost("guest")]
    public async Task<ActionResult<LoginResponse>> GuestLogin(
        CancellationToken cancellationToken)
    {
        var response = await _authService.GuestLoginAsync(
            cancellationToken);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
        CancellationToken cancellationToken)
    {
        await _authService.LogoutAsync(
            request.RefreshToken,
            cancellationToken);

        return Ok(new
        {
            Message = "Logged out successfully."
        });
    }
}