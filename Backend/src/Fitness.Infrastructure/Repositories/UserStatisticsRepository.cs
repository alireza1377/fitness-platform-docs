using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class UserStatisticsRepository : IUserStatisticsRepository
{
    private readonly FitnessDbContext _context;

    public UserStatisticsRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<UserStatistics?> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserStatistics
            .FirstOrDefaultAsync(
                x => x.UserId == userId,
                cancellationToken);
    }

    public async Task AddAsync(
        UserStatistics statistics,
        CancellationToken cancellationToken = default)
    {
        await _context.UserStatistics.AddAsync(
            statistics,
            cancellationToken);
    }

    public Task UpdateAsync(
        UserStatistics statistics,
        CancellationToken cancellationToken = default)
    {
        _context.UserStatistics.Update(statistics);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}