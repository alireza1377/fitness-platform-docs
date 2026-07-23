using Fitness.Application.DTOs.Storage;

namespace Fitness.Application.Interfaces;

public interface IVideoUploadService
{
    Task<UploadVideoResponse> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);
}