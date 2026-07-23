using Fitness.Application.DTOs.Subscription;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class SubscriptionPlanService : ISubscriptionPlanService
{
    private readonly ISubscriptionPlanRepository _repository;

    public SubscriptionPlanService(
        ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SubscriptionPlanDto>> GetPlansAsync(
        CancellationToken cancellationToken = default)
    {
        var plans =
            await _repository.GetAllAsync(cancellationToken);

        return plans
            .Where(x => x.IsActive)
            .Select(x => new SubscriptionPlanDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                DurationDays = x.DurationDays,
                Type = x.Type.ToString()
            })
            .ToList();
    }
}