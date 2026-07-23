using Microsoft.AspNetCore.Http;

namespace Fitness.Application.DTOs.Videos;

public class UploadVideoRequest
{
    public IFormFile Video { get; set; } = default!;

    public IFormFile? Thumbnail { get; set; }
}
