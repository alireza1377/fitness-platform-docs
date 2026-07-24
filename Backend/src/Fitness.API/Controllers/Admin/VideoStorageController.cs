using Fitness.API.DTOs.Videos;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        if (request.Video == null || request.Video.Length == 0)
            return BadRequest("Video is required.");

        await using var videoStream = request.Video.OpenReadStream();

        Stream? thumbnailStream = null;

        if (request.Thumbnail != null)
            thumbnailStream = request.Thumbnail.OpenReadStream();

        var storageId = await _videoStorageService.UploadAsync(
            videoStream,
            request.Video.FileName,
            request.Video.ContentType,
            thumbnailStream,
            request.Thumbnail?.FileName,
            cancellationToken);

        return Ok(new
        {
            storageId
        });
    }
}