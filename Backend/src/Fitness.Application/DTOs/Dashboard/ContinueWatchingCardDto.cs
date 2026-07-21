namespace Fitness.Application.DTOs.Dashboard;

public class ContinueWatchingCardDto
{
    public Guid VideoId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int PositionSeconds { get; set; }

    public int DurationSeconds { get; set; }

    public string? ThumbnailUrl { get; set; }
}