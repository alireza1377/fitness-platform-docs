using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IVideoStorageRepository
{
    Task<VideoStorage?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<VideoStorage?> GetByFileKeyAsync(
        string fileKey,
        CancellationToken cancellationToken = default);

    Task<List<VideoStorage>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        VideoStorage entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        VideoStorage entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        VideoStorage entity,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}