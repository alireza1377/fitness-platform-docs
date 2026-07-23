using Fitness.Application.DTOs.Storage;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class VideoUploadService : IVideoUploadService
{
    private readonly IFileStorageService _fileStorageService;

    public VideoUploadService(
        IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task<UploadVideoResponse> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var storage = await _fileStorageService.UploadAsync(
            stream,
            fileName,
            contentType,
            cancellationToken);

        return new UploadVideoResponse
        {
            StorageId = storage.Id,

            StreamUrl =
                await _fileStorageService.GetStreamingUrlAsync(
                    storage.Id,
                    cancellationToken),

            DownloadUrl =
                await _fileStorageService.GetDownloadUrlAsync(
                    storage.Id,
                    cancellationToken),

            FileName = storage.OriginalFileName,

            FileSize = storage.FileSize
        };
    }
}