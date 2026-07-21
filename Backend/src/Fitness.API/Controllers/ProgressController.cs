using Fitness.Application.DTOs.Progress;
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

    [HttpGet("programs/{programId:guid}")]
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

    [HttpPost("programs/{programId:guid}/complete")]
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

    [HttpPost("videos/{videoId:guid}/complete")]
    public async Task<IActionResult> CompleteVideo(
        Guid videoId,
        CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
            return Unauthorized();

        await _progressService.CompleteVideoAsync(
            _currentUserService.UserId.Value,
            videoId,
            cancellationToken);

        return Ok(new
        {
            Message = "Video completed successfully."
        });
    }

    [HttpPut("videos/{videoId:guid}/position")]
    public async Task<IActionResult> UpdateVideoPosition(
        Guid videoId,
        [FromBody] UpdateVideoPositionRequest request,
        CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
            return Unauthorized();

        await _progressService.UpdateVideoPositionAsync(
            _currentUserService.UserId.Value,
            videoId,
            request.CurrentPositionSeconds,
            cancellationToken);

        return NoContent();
    }
}