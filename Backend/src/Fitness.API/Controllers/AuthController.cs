using Fitness.Application.DTOs.Auth;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IOtpService _otpService;
    private readonly IAuthService _authService;

    public AuthController(
        IOtpService otpService,
        IAuthService authService)
    {
        _otpService = otpService;
        _authService = authService;
    }

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
        message = "OTP sent successfully."
    });
}

    [HttpPost("verify-otp")]
    public async Task<ActionResult<LoginResponse>> VerifyOtp(
        [FromBody] VerifyOtpRequest request,
        CancellationToken cancellationToken)
    {
        var response =
            await _authService.VerifyOtpAsync(
                request.PhoneNumber,
                request.Code,
                cancellationToken);

        return Ok(response);
    }

    [HttpPost("guest")]
    public async Task<ActionResult<LoginResponse>> GuestLogin(
        CancellationToken cancellationToken)
    {
        var response =
            await _authService.GuestLoginAsync(
                cancellationToken);

        return Ok(response);
    }
}