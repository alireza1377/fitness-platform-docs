namespace Fitness.Application.DTOs.Dashboard;

public class DashboardDto
{
    public UserCardDto User { get; set; } = new();

    public BmiCardDto Bmi { get; set; } = new();

    public StatisticsCardDto Statistics { get; set; } = new();

    public CurrentProgramCardDto? CurrentProgram { get; set; }

    public ContinueWatchingCardDto? ContinueWatching { get; set; }

    // Phase 2

    public SubscriptionCardDto? Subscription { get; set; }

    public TodayWorkoutCardDto? TodayWorkout { get; set; }

    public List<NotificationCardDto> Notifications { get; set; } = [];

    // Phase 3

    public NutritionCardDto? Nutrition { get; set; }

    public WaterCardDto? Water { get; set; }

    public StreakCardDto? Streak { get; set; }

    public AiCoachCardDto? AiCoach { get; set; }
}