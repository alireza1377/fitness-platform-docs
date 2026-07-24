using System.IO;
using System.Linq;
using Fitness.API.DTOs.Videos;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers.Admin;

[ApiController]
[Route("api/admin/videos")]
//[Authorize(Roles = "Admin")]
public class VideoStorageController : ControllerBase
{
    private readonly IVideoStorageService _videoStorageService;

    private static readonly string[] AllowedVideoExtensions =
    {
        ".mp4",
        ".mov",
        ".mkv",
        ".webm"
    };

    private static readonly string[] AllowedImageExtensions =
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

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

        // بررسی فرمت ویدئو
        var videoExtension =
            Path.GetExtension(request.Video.FileName)
                .ToLowerInvariant();

        if (!AllowedVideoExtensions.Contains(videoExtension))
        {
            return BadRequest(
                $"Video format '{videoExtension}' is not supported.");
        }

        // بررسی MIME Type ویدئو
        if (!request.Video.ContentType.StartsWith("video/"))
        {
            return BadRequest("Invalid video file.");
        }

        // بررسی Thumbnail
        if (request.Thumbnail != null)
        {
            var imageExtension =
                Path.GetExtension(request.Thumbnail.FileName)
                    .ToLowerInvariant();

            if (!AllowedImageExtensions.Contains(imageExtension))
            {
                return BadRequest(
                    $"Thumbnail format '{imageExtension}' is not supported.");
            }

            if (!request.Thumbnail.ContentType.StartsWith("image/"))
            {
                return BadRequest("Invalid thumbnail image.");
            }
        }

        await using var videoStream =
            request.Video.OpenReadStream();

        Stream? thumbnailStream = null;

        if (request.Thumbnail != null)
        {
            thumbnailStream =
                request.Thumbnail.OpenReadStream();
        }

        var storageId =
            await _videoStorageService.UploadAsync(
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