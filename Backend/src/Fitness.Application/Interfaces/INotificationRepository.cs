using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface INotificationRepository
{
    Task<List<Notification>> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<int> GetUnreadCountAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<Notification?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Notification notification,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Notification notification,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}