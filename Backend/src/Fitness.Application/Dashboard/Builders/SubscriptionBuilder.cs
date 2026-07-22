using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Dashboard.Builders;

public class SubscriptionBuilder
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionBuilder(
        ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<SubscriptionCardDto?> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var subscription =
            await _subscriptionRepository.GetByUserAsync(
                userId,
                cancellationToken);

        if (subscription is null)
            return null;

        return new SubscriptionCardDto
        {
            IsPremium = subscription.IsActive(),
            ExpireAt = subscription.EndDate,
            RemainingDays = subscription.RemainingDays()
        };
    }
}