using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/storage")]
public class AdminVideoController : ControllerBase
{
    private readonly IVideoUploadService _videoUploadService;

    public AdminVideoController(
        IVideoUploadService videoUploadService)
    {
        _videoUploadService = videoUploadService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

        await using var stream = file.OpenReadStream();

        var result = await _videoUploadService.UploadAsync(
            stream,
            file.FileName,
            file.ContentType,
            cancellationToken);

        return Ok(result);
    }
}