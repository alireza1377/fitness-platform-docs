namespace Fitness.Application.DTOs.Dashboard;

public class StatisticsCardDto
{
    public int CompletedPrograms { get; set; }

    public int InProgressPrograms { get; set; }

    public int CompletedVideos { get; set; }

    public int WorkoutMinutes { get; set; }

    public int CaloriesBurned { get; set; }
}