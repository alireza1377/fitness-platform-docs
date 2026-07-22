using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}