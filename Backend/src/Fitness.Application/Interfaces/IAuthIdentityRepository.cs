using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IAuthIdentityRepository
{
    Task<AuthIdentity?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        AuthIdentity authIdentity,
        CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
    CancellationToken cancellationToken = default);
        
}