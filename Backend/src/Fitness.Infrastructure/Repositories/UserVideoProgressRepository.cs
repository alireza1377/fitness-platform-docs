using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class UserVideoProgressRepository : IUserVideoProgressRepository
{
    private readonly FitnessDbContext _context;

    public UserVideoProgressRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<UserVideoProgress?> GetAsync(
        Guid userId,
        Guid programVideoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserVideoProgresses
            .FirstOrDefaultAsync(
                x =>
                    x.UserId == userId &&
                    x.ProgramVideoId == programVideoId,
                cancellationToken);
    }

    public async Task AddAsync(
        UserVideoProgress progress,
        CancellationToken cancellationToken = default)
    {
        await _context.UserVideoProgresses.AddAsync(
            progress,
            cancellationToken);
    }

 
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

   public Task UpdateAsync(
    UserVideoProgress progress,
    CancellationToken cancellationToken = default)
{
    _context.UserVideoProgresses.Update(progress);

    return Task.CompletedTask;
}

public async Task<int> CountCompletedVideosAsync(
    Guid userId,
    Guid programId,
    CancellationToken cancellationToken = default)
{
    return await _context.UserVideoProgresses
        .CountAsync(
            x =>
                x.UserId == userId &&
                x.ProgramVideo.FitnessProgramId == programId &&
                x.Completed,
            cancellationToken);
}
}