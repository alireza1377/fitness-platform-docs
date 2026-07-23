using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class VideoStorage : AuditableEntity
{
    public StorageProvider StorageProvider { get; private set; }

    public StorageStatus Status { get; private set; }

    public string Bucket { get; private set; } = string.Empty;

    public string Region { get; private set; } = string.Empty;

    public string FileKey { get; private set; } = string.Empty;

    public string OriginalFileName { get; private set; } = string.Empty;

    public string ContentType { get; private set; } = string.Empty;

    public long FileSize { get; private set; }

    public string Checksum { get; private set; } = string.Empty;

    public int DurationSeconds { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public int Bitrate { get; private set; }

    public string? ThumbnailUrl { get; private set; }

    public string? CdnUrl { get; private set; }

    public ICollection<ProgramVideo> ProgramVideos { get; private set; }
        = new List<ProgramVideo>();

    private VideoStorage()
    {
    }

    public VideoStorage(
        StorageProvider storageProvider,
        string bucket,
        string region,
        string fileKey,
        string originalFileName,
        string contentType,
        long fileSize,
        string checksum,
        int durationSeconds,
        int width,
        int height,
        int bitrate,
        string? thumbnailUrl,
        string? cdnUrl)
    {
        StorageProvider = storageProvider;
        Bucket = bucket;
        Region = region;
        FileKey = fileKey;
        OriginalFileName = originalFileName;
        ContentType = contentType;
        FileSize = fileSize;
        Checksum = checksum;
        DurationSeconds = durationSeconds;
        Width = width;
        Height = height;
        Bitrate = bitrate;
        ThumbnailUrl = thumbnailUrl;
        CdnUrl = cdnUrl;

        Status = StorageStatus.Uploading;
    }

    public void MarkReady()
    {
        Status = StorageStatus.Ready;
        SetUpdated();
    }

    public void MarkFailed()
    {
        Status = StorageStatus.Failed;
        SetUpdated();
    }

    public void MarkDeleted()
    {
        Status = StorageStatus.Deleted;
        SetUpdated();
    }

    public void Move(
        StorageProvider provider,
        string bucket,
        string region,
        string fileKey,
        string? cdnUrl)
    {
        StorageProvider = provider;
        Bucket = bucket;
        Region = region;
        FileKey = fileKey;
        CdnUrl = cdnUrl;

        SetUpdated();
    }

    public void UpdateMetadata(
        int durationSeconds,
        int width,
        int height,
        int bitrate,
        string checksum)
    {
        DurationSeconds = durationSeconds;
        Width = width;
        Height = height;
        Bitrate = bitrate;
        Checksum = checksum;

        SetUpdated();
    }
}