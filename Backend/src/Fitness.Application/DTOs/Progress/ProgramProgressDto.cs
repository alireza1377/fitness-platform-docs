namespace Fitness.Application.DTOs.Progress;

public class ProgramProgressDto
{
    public Guid ProgramId { get; set; }

    public int TotalVideos { get; set; }

    public int CompletedVideos { get; set; }

    public double Percentage { get; set; }

    public bool IsCompleted { get; set; }
}