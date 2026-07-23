using Fitness.Application.DTOs.Payment;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Authorize]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ICurrentUserService _currentUser;

    public PaymentController(
        IPaymentService paymentService,
        ICurrentUserService currentUser)
    {
        _paymentService = paymentService;
        _currentUser = currentUser;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] CreatePaymentRequestDto request,
        CancellationToken cancellationToken)
    {
        if (_currentUser.UserId is null)
            return Unauthorized();

        var result = await _paymentService.CreatePaymentAsync(
            _currentUser.UserId.Value,
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify(
        [FromBody] VerifyPaymentRequestDto request,
        CancellationToken cancellationToken)
    {
        await _paymentService.VerifyPaymentAsync(
            request.PaymentId,
            request.Authority,
            cancellationToken);

        return Ok(new
        {
            success = true,
            message = "Payment verified successfully."
        });
    }
    [HttpGet("history")]
public async Task<IActionResult> History(
    CancellationToken cancellationToken)
{
    if (_currentUser.UserId is null)
        return Unauthorized();

    var result =
        await _paymentService.GetHistoryAsync(
            _currentUser.UserId.Value,
            cancellationToken);

    return Ok(result);
}
}