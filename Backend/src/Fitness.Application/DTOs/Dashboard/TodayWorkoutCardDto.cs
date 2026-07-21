namespace Fitness.Application.DTOs.Dashboard;

public class TodayWorkoutCardDto
{
    public Guid ProgramId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int DayNumber { get; set; }

    public int EstimatedMinutes { get; set; }
}