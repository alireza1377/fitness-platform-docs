using Fitness.Application.DTOs.Subscription;

namespace Fitness.Application.Interfaces;

public interface ISubscriptionPlanService
{
    Task<List<SubscriptionPlanDto>> GetPlansAsync(
        CancellationToken cancellationToken = default);
}