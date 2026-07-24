namespace Fitness.Application.Interfaces;

public interface IThumbnailGenerator
{
    Task<string> GenerateAsync(
        string videoPath,
        CancellationToken cancellationToken = default);
}