using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class AuthIdentityRepository : IAuthIdentityRepository
{
    private readonly FitnessDbContext _context;

    public AuthIdentityRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<AuthIdentity?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<AuthIdentity>()
            .FirstOrDefaultAsync(
                x => x.UserId == userId,
                cancellationToken);
    }

    public async Task AddAsync(
        AuthIdentity authIdentity,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<AuthIdentity>()
            .AddAsync(authIdentity, cancellationToken);
    }
    
    public async Task SaveChangesAsync(
    CancellationToken cancellationToken = default)
{
    await _context.SaveChangesAsync(cancellationToken);
}
}