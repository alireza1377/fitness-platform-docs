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
        
        Task UpdateAsync(
    UserProgramProgress progress,
    CancellationToken cancellationToken = default);

    Task<List<UserProgramProgress>> GetByUserAsync(
    Guid userId,
    CancellationToken cancellationToken = default);

Task<int> CountCompletedAsync(
    Guid userId,
    CancellationToken cancellationToken = default);

    Task<UserProgramProgress?> GetCurrentProgramAsync(
    Guid userId,
    CancellationToken cancellationToken = default);
    
}