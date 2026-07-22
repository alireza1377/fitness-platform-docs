using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Dashboard.Builders;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly UserCardBuilder _userCardBuilder;
    private readonly StatisticsBuilder _statisticsBuilder;
    private readonly CurrentProgramBuilder _currentProgramBuilder;
    private readonly WorkoutBuilder _workoutBuilder;
    private readonly SubscriptionBuilder _subscriptionBuilder;
    private readonly NotificationBuilder _notificationBuilder;
    private readonly QuickActionBuilder _quickActionBuilder;
    private readonly BmiBuilder _bmiBuilder;

    private readonly IUserVideoProgressRepository _videoProgressRepository;

    public DashboardService(
        UserCardBuilder userCardBuilder,
        StatisticsBuilder statisticsBuilder,
        CurrentProgramBuilder currentProgramBuilder,
        WorkoutBuilder workoutBuilder,
        SubscriptionBuilder subscriptionBuilder,
        NotificationBuilder notificationBuilder,
        QuickActionBuilder quickActionBuilder,
        BmiBuilder bmiBuilder,
        IUserVideoProgressRepository videoProgressRepository)
    {
        _userCardBuilder = userCardBuilder;
        _statisticsBuilder = statisticsBuilder;
        _currentProgramBuilder = currentProgramBuilder;
        _workoutBuilder = workoutBuilder;
        _subscriptionBuilder = subscriptionBuilder;
        _notificationBuilder = notificationBuilder;
        _quickActionBuilder = quickActionBuilder;
        _videoProgressRepository = videoProgressRepository;
        _bmiBuilder = bmiBuilder;
    }

    public async Task<DashboardDto> GetDashboardAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var dashboard = new DashboardDto();

        // User
        dashboard.User =
            await _userCardBuilder.BuildAsync(
                userId,
                cancellationToken);
        // BMI
         dashboard.Bmi =
             await _bmiBuilder.BuildAsync(
                userId,
                cancellationToken);

        // Statistics
        var statistics =
            await _statisticsBuilder.BuildAsync(
                userId,
                cancellationToken);

        dashboard.Statistics = statistics.Item1;
        dashboard.Streak = statistics.Item2;

        // Current Program
        dashboard.CurrentProgram =
            await _currentProgramBuilder.BuildAsync(
                userId,
                cancellationToken);

        // Today Workout
        dashboard.TodayWorkout =
            await _workoutBuilder.BuildAsync(
                userId,
                cancellationToken);

        // Subscription
        dashboard.Subscription =
            await _subscriptionBuilder.BuildAsync(
                userId,
                cancellationToken);

        // Continue Watching
        var lastVideo =
            await _videoProgressRepository.GetLastWatchedAsync(
                userId,
                cancellationToken);

        if (lastVideo is not null)
        {
            dashboard.ContinueWatching =
                new ContinueWatchingCardDto
                {
                    VideoId = lastVideo.ProgramVideoId,
                    Title = lastVideo.ProgramVideo.Title,
                    PositionSeconds = lastVideo.CurrentPositionSeconds,
                    DurationSeconds =
                        (int)lastVideo.ProgramVideo.Duration.TotalSeconds,
                    ThumbnailUrl =
                        lastVideo.ProgramVideo.ThumbnailUrl
                };
        }

        // Notifications
        dashboard.Notifications =
            await _notificationBuilder.BuildAsync(
                userId,
                cancellationToken);

        // Quick Actions
        dashboard.QuickActions =
            _quickActionBuilder.Build();

        return dashboard;
    }
}