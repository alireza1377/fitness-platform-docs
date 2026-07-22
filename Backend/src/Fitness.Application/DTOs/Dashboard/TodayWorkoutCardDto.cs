namespace Fitness.Application.DTOs.Dashboard;

public class TodayWorkoutCardDto
{
    public Guid ProgramId { get; set; }

    public string ProgramTitle { get; set; } = string.Empty;

    public string VideoTitle { get; set; } = string.Empty;

    public int DayNumber { get; set; }

    public int EstimatedMinutes { get; set; }
}