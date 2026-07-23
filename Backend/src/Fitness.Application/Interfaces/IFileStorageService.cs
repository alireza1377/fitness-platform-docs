using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IFileStorageService
{
    Task<VideoStorage> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid storageId,
        CancellationToken cancellationToken = default);

    Task<string> GetStreamingUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default);

    Task<string?> GetDownloadUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        Guid storageId,
        CancellationToken cancellationToken = default);
}