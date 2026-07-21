namespace Fitness.Application.DTOs.Content;

public class FitnessProgramDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public string? Description { get; set; }

    public int VideosCount { get; set; }
}