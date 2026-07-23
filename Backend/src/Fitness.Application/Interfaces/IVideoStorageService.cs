namespace Fitness.Application.Interfaces;

public interface IVideoStorageService
{
    Task<Guid> UploadAsync(
        Stream videoStream,
        string fileName,
        string contentType,
        Stream? thumbnailStream,
        string? thumbnailFileName,
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