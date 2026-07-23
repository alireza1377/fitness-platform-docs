using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class SubscriptionPlanRepository
    : ISubscriptionPlanRepository
{
    private readonly FitnessDbContext _context;

    public SubscriptionPlanRepository(
        FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionPlan?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionPlans
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<List<SubscriptionPlan>> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionPlans
            .Where(x => x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        SubscriptionPlan plan,
        CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionPlans.AddAsync(
            plan,
            cancellationToken);
    }

    public Task UpdateAsync(
        SubscriptionPlan plan,
        CancellationToken cancellationToken = default)
    {
        _context.SubscriptionPlans.Update(plan);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<List<SubscriptionPlan>> GetAllAsync(
    CancellationToken cancellationToken = default)
{
    return await _context.SubscriptionPlans
        .OrderBy(x => x.Price)
        .ToListAsync(cancellationToken);
}
}
