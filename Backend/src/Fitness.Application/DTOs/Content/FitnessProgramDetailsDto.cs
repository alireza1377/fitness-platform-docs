namespace Fitness.Application.DTOs.Content;

public class FitnessProgramDetailsDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryTitle { get; set; } = string.Empty;

    public int VideosCount { get; set; }
}