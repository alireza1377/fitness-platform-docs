using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class UserProgramProgressRepository
    : IUserProgramProgressRepository
{
    private readonly FitnessDbContext _context;

    public UserProgramProgressRepository(
        FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<UserProgramProgress?> GetAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserProgramProgresses
            .FirstOrDefaultAsync(
                x =>
                    x.UserId == userId &&
                    x.FitnessProgramId == programId,
                cancellationToken);
    }

    public async Task AddAsync(
        UserProgramProgress progress,
        CancellationToken cancellationToken = default)
    {
        await _context.UserProgramProgresses.AddAsync(
            progress,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}