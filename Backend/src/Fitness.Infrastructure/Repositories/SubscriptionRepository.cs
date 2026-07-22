using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly FitnessDbContext _context;

    public SubscriptionRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<Subscription?> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Subscriptions
            .FirstOrDefaultAsync(
                x => x.UserId == userId,
                cancellationToken);
    }

    public async Task AddAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default)
    {
        await _context.Subscriptions.AddAsync(
            subscription,
            cancellationToken);
    }

    public Task UpdateAsync(
        Subscription subscription,
        CancellationToken cancellationToken = default)
    {
        _context.Subscriptions.Update(subscription);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}