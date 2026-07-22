using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IUserStatisticsRepository _statisticsRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUserProgramProgressRepository _progressRepository;

    public NotificationService(
        IUserStatisticsRepository statisticsRepository,
        ISubscriptionRepository subscriptionRepository,
        IUserProgramProgressRepository progressRepository)
    {
        _statisticsRepository = statisticsRepository;
        _subscriptionRepository = subscriptionRepository;
        _progressRepository = progressRepository;
    }

    public async Task<List<NotificationCardDto>> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var notifications = new List<NotificationCardDto>();

        //---------------------------------------------------
        // Subscription
        //---------------------------------------------------

        var subscription =
            await _subscriptionRepository.GetByUserAsync(
                userId,
                cancellationToken);

        if (subscription != null &&
            subscription.IsActive() &&
            subscription.RemainingDays() <= 7)
        {
            notifications.Add(new NotificationCardDto
            {
                Title = "Subscription",

                Message =
                    $"Your subscription expires in {subscription.RemainingDays()} days.",

                Type = NotificationType.Subscription
            });
        }

        //---------------------------------------------------
        // Workout
        //---------------------------------------------------

        var statistics =
            await _statisticsRepository.GetByUserAsync(
                userId,
                cancellationToken);

        if (statistics != null &&
            statistics.LastWorkoutDate != DateOnly.FromDateTime(DateTime.UtcNow))
        {
            notifications.Add(new NotificationCardDto
            {
                Title = "Workout",

                Message = "You haven't completed today's workout.",

                Type = NotificationType.Workout
            });
        }

        //---------------------------------------------------
        // Completed Program
        //---------------------------------------------------

        var completed =
            await _progressRepository.CountCompletedAsync(
                userId,
                cancellationToken);

        if (completed > 0)
        {
            notifications.Add(new NotificationCardDto
            {
                Title = "Great Job!",

                Message =
                    $"You have completed {completed} program(s).",

                Type = NotificationType.Success
            });
        }

        return notifications;
    }
}