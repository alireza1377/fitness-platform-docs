using Microsoft.AspNetCore.Http;

namespace Fitness.API.DTOs.Videos;

public class UploadVideoRequest
{
    public IFormFile Video { get; set; } = default!;

    public IFormFile? Thumbnail { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool IsFree { get; set; }
}