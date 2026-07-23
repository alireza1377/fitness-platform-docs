using Fitness.Domain.Enums;

namespace Fitness.Application.Interfaces;

public interface ISubscriptionActivationService
{
    Task CreateSubscriptionAsync(
        Guid userId,
        SubscriptionType planType,
        string paymentReference,
        CancellationToken cancellationToken = default);
}