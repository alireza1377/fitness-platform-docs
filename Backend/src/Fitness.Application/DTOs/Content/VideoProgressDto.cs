namespace Fitness.Application.DTOs.Content;

public class VideoProgressDto
{
    public Guid VideoId { get; set; }

    public int CurrentPositionSeconds { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime LastWatchedAt { get; set; }
}
