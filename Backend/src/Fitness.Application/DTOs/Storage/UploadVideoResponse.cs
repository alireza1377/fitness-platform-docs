namespace Fitness.Application.DTOs.Storage;

public sealed class UploadVideoResponse
{
    public Guid StorageId { get; init; }

    public string StreamUrl { get; init; } = string.Empty;

    public string? DownloadUrl { get; init; }

    public string FileName { get; init; } = string.Empty;

    public long FileSize { get; init; }
}