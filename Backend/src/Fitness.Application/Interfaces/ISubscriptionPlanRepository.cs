using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface ISubscriptionPlanRepository
{
    Task<SubscriptionPlan?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<List<SubscriptionPlan>> GetActiveAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        SubscriptionPlan plan,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        SubscriptionPlan plan,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);

        Task<List<SubscriptionPlan>> GetAllAsync(
    CancellationToken cancellationToken = default);
}