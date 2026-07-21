using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Authorize]
[Route("api/progress")]
public class ProgressController : ControllerBase
{
    private readonly IProgressService _progressService;
    private readonly ICurrentUserService _currentUserService;

    public ProgressController(
        IProgressService progressService,
        ICurrentUserService currentUserService)
    {
        _progressService = progressService;
        _currentUserService = currentUserService;
    }

    [HttpGet("{programId:guid}")]
    public async Task<IActionResult> GetProgress(
        Guid programId,
        CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
            return Unauthorized();

        var progress = await _progressService.GetProgressAsync(
            _currentUserService.UserId.Value,
            programId,
            cancellationToken);

        return Ok(progress);
    }

    [HttpPost("{programId:guid}/complete")]
    public async Task<IActionResult> CompleteProgram(
        Guid programId,
        CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
            return Unauthorized();

        await _progressService.CompleteProgramAsync(
            _currentUserService.UserId.Value,
            programId,
            cancellationToken);

        return Ok(new
        {
            Message = "Program completed successfully."
        });
    }
}