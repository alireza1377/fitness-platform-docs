using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/subscription-plans")]
public class SubscriptionPlansController : ControllerBase
{
    private readonly ISubscriptionPlanService _service;

    public SubscriptionPlansController(
        ISubscriptionPlanService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlans(
        CancellationToken cancellationToken)
    {
        var plans =
            await _service.GetPlansAsync(cancellationToken);

        return Ok(plans);
    }
}