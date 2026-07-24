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
    
    var thumbnailPath = await _thumbnailGenerator.GenerateAsync(
    filePath,
    cancellationToken);

    storage.SetThumbnail(thumbnailPath);
    
    // در مراحل بعد اگر thumbnailStream وجود داشت
    // اینجا آن را نیز ذخیره خواهیم کرد.

    await _videoStorageRepository.AddAsync(
        storage,
        cancellationToken);

    await _videoStorageRepository.SaveChangesAsync(
        cancellationToken);

    return storage.Id;
}
    public async Task DeleteAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetStreamingUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return await _fileStorageService.GetStreamingUrlAsync(
            storageId,
            cancellationToken);
    }

    public async Task<string?> GetDownloadUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return await _fileStorageService.GetDownloadUrlAsync(
            storageId,
            cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return await _fileStorageService.ExistsAsync(
            storageId,
            cancellationToken);
    }
}