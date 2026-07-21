using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IUserVideoProgressRepository
{
    Task<UserVideoProgress?> GetAsync(
        Guid userId,
        Guid programVideoId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        UserVideoProgress progress,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UserVideoProgress progress,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);

        Task<int> CountCompletedVideosAsync(
    Guid userId,
    Guid programId,
    CancellationToken cancellationToken = default);
}