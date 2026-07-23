using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class VideoStorageService : IVideoStorageService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IVideoStorageRepository _videoStorageRepository;

    public VideoStorageService(
        IFileStorageService fileStorageService,
        IVideoStorageRepository videoStorageRepository)
    {
        _fileStorageService = fileStorageService;
        _videoStorageRepository = videoStorageRepository;
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