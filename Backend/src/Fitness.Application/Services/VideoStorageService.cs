using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class VideoStorageService : IVideoStorageService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IVideoStorageRepository _videoStorageRepository;
    private readonly IVideoMetadataService _videoMetadataService;
    private readonly IThumbnailGenerator _thumbnailGenerator;

    public VideoStorageService(
        IFileStorageService fileStorageService,
        IVideoStorageRepository videoStorageRepository,
        IVideoMetadataService videoMetadataService,
        IThumbnailGenerator thumbnailGenerator)
    {
        _fileStorageService = fileStorageService;
        _videoStorageRepository = videoStorageRepository;
        _videoMetadataService = videoMetadataService;
        _thumbnailGenerator = thumbnailGenerator;
    }

    public async Task<Guid> UploadAsync(
        Stream videoStream,
        string fileName,
        string contentType,
        Stream? thumbnailStream,
        string? thumbnailFileName,
        CancellationToken cancellationToken = default)
    {
        var storage = await _fileStorageService.UploadAsync(
            videoStream,
            fileName,
            contentType,
            cancellationToken);

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "storage",
            storage.FileKey);

        var metadata = await _videoMetadataService.ExtractAsync(
            filePath,
            cancellationToken);

        storage.UpdateMetadata(
            metadata.DurationSeconds,
            metadata.Width,
            metadata.Height,
            metadata.Bitrate,
            storage.Checksum);

        // اگر Thumbnail توسط کاربر ارسال شده باشد
        if (thumbnailStream != null &&
            !string.IsNullOrWhiteSpace(thumbnailFileName))
        {
           var thumbnailFileKey =
   
await _fileStorageService.UploadThumbnailAsync(
    storage.FileKey,
    thumbnailStream,
    thumbnailFileName,
    cancellationToken);
    
            storage.SetThumbnail(thumbnailFileKey);
        }
        else
        {
            // در غیر این صورت از روی ویدئو ساخته می‌شود
            var thumbnailPath =
                await _thumbnailGenerator.GenerateAsync(
                    filePath,
                    cancellationToken);

            storage.SetThumbnail(
                Path.GetFileName(thumbnailPath));
        }

        await _videoStorageRepository.AddAsync(
            storage,
            cancellationToken);

        await _videoStorageRepository.SaveChangesAsync(
            cancellationToken);

        return storage.Id;
    }

    public Task DeleteAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetStreamingUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return _fileStorageService.GetStreamingUrlAsync(
            storageId,
            cancellationToken);
    }

    public Task<string?> GetDownloadUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return _fileStorageService.GetDownloadUrlAsync(
            storageId,
            cancellationToken);
    }

    public Task<bool> ExistsAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return _fileStorageService.ExistsAsync(
            storageId,
            cancellationToken);
    }
}