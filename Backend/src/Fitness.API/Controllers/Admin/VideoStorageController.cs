using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fitness.Application.DTOs.Videos;
namespace Fitness.API.Controllers.Admin;

[ApiController]
[Route("api/admin/videos")]
[Authorize(Roles = "Admin")]
public class VideoStorageController : ControllerBase
{
    private readonly IVideoStorageService _videoStorageService;

    public VideoStorageController(
        IVideoStorageService videoStorageService)
    {
        _videoStorageService = videoStorageService;
    }

    [HttpPost("upload")]
[RequestSizeLimit(5_000_000_000)]
public async Task<IActionResult> Upload(
    [FromForm] UploadVideoRequest request,
    CancellationToken cancellationToken)
{
    if (request.Video is null || request.Video.Length == 0)
        return BadRequest("Video is required.");

    var storageId = await _videoStorageService.UploadAsync(
        request.Video,
        cancellationToken);

    return Ok(new
    {
        storageId,
        hasCustomThumbnail = request.Thumbnail != null
    });
}
}