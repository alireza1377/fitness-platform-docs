using Fitness.Application.DTOs.Videos;
namespace Fitness.Application.Interfaces;

public interface IVideoMetadataService
{
    Task<VideoMetadataResult> ExtractAsync(
        string filePath,
        CancellationToken cancellationToken = default);
}