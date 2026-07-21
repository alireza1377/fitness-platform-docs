using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IUserStatisticsRepository
{
    Task<UserStatistics?> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        UserStatistics statistics,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UserStatistics statistics,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}