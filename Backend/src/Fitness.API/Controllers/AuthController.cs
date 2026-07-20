using Fitness.Application.DTOs.Auth;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IOtpService _otpService;

    public AuthController(IOtpService otpService)
    {
        _otpService = otpService;
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp(
        [FromBody] SendOtpRequest request,
        CancellationToken cancellationToken)
    {
        await _otpService.SendOtpAsync(
            request.PhoneNumber,
            cancellationToken);

        return Ok(new
        {
            Message = "OTP sent successfully."
        });
    }
    [HttpPost("verify-otp")]
public async Task<IActionResult> VerifyOtp(
    [FromBody] VerifyOtpRequest request,
    CancellationToken cancellationToken)
{
    await _otpService.VerifyOtpAsync(
        request.PhoneNumber,
        request.Code,
        cancellationToken);

    return Ok(new
    {
        Message = "OTP verified successfully."
    });
}
}