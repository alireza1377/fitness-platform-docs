using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IUserProgramProgressRepository
{
    Task<UserProgramProgress?> GetAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        UserProgramProgress progress,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}