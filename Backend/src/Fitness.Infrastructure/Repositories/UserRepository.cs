using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FitnessDbContext _context;

    public UserRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(
                x => x.PhoneNumber == phoneNumber,
                cancellationToken);
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<User>()
            .AddAsync(user, cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default)
{
    return await _context.Users
        .FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken);
}

public void Update(User user)
{
    _context.Users.Update(user);
}

}