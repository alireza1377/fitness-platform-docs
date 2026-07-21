namespace Fitness.Application.DTOs.Dashboard;

public class CurrentProgramCardDto
{
    public Guid ProgramId { get; set; }

    public string Title { get; set; } = string.Empty;

    public double Progress { get; set; }

    public int CompletedVideos { get; set; }

    public int TotalVideos { get; set; }
}