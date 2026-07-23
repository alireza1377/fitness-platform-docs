using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Domain.Enums;

namespace Fitness.Application.Services;

public class SubscriptionActivationService : ISubscriptionActivationService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly INotificationRepository _notificationRepository;

    public SubscriptionActivationService(
        ISubscriptionRepository subscriptionRepository,
        INotificationRepository notificationRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task CreateSubscriptionAsync(
        Guid userId,
        SubscriptionType planType,
        string paymentReference,
        CancellationToken cancellationToken = default)
    {
        var duration = GetDuration(planType);

        var subscription = new Subscription(
            userId,
            planType,
            DateTime.UtcNow,
            DateTime.UtcNow.Add(duration),
            false,
            paymentReference);

        await _subscriptionRepository.AddAsync(
            subscription,
            cancellationToken);

        await _subscriptionRepository.SaveChangesAsync(
            cancellationToken);

        // مرحله بعد Notification واقعی اینجا ثبت می‌شود.
    }

  private static TimeSpan GetDuration(
    SubscriptionType planType)
{
    return planType switch
    {
        SubscriptionType.Free =>
            TimeSpan.FromDays(36500),

        SubscriptionType.PremiumMonthly =>
            TimeSpan.FromDays(30),

        SubscriptionType.PremiumQuarterly =>
            TimeSpan.FromDays(90),

        SubscriptionType.PremiumYearly =>
            TimeSpan.FromDays(365),

        _ =>
            TimeSpan.FromDays(30)
    };
}
}