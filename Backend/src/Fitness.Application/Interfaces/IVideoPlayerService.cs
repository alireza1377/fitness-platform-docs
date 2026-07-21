using Fitness.Application.DTOs.Content;

namespace Fitness.Application.Interfaces;

public interface IVideoPlayerService
{
    Task<ProgramVideoDto?> GetVideoAsync(
        Guid userId,
        Guid videoId,
        CancellationToken cancellationToken = default);

    Task SaveProgressAsync(
        Guid userId,
        Guid videoId,
        int currentPositionSeconds,
        CancellationToken cancellationToken = default);

    Task CompleteVideoAsync(
        Guid userId,
        Guid videoId,
        CancellationToken cancellationToken = default);
}