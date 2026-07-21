using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/content")]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(
        CancellationToken cancellationToken)
    {
        var result =
            await _contentService.GetCategoriesAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("categories/{categoryId:guid}/programs")]
    public async Task<IActionResult> GetPrograms(
        Guid categoryId,
        CancellationToken cancellationToken)
    {
        var result =
            await _contentService.GetProgramsAsync(
                categoryId,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("programs/{programId:guid}")]
    public async Task<IActionResult> GetProgram(
        Guid programId,
        CancellationToken cancellationToken)
    {
        var result =
            await _contentService.GetProgramAsync(
                programId,
                cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("programs/{programId:guid}/videos")]
    public async Task<IActionResult> GetVideos(
        Guid programId,
        CancellationToken cancellationToken)
    {
        var result =
            await _contentService.GetVideosAsync(
                programId,
                cancellationToken);

        return Ok(result);
    }
}