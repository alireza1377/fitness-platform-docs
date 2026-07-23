using Fitness.Application.DTOs.Content;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class VideoPlayerService : IVideoPlayerService
{
    private readonly IProgramVideoRepository _videoRepository;
    private readonly IUserVideoProgressRepository _progressRepository;

    public VideoPlayerService(
        IProgramVideoRepository videoRepository,
        IUserVideoProgressRepository progressRepository)
    {
        _videoRepository = videoRepository;
        _progressRepository = progressRepository;
    }

    public async Task<ProgramVideoDto?> GetVideoAsync(
        Guid userId,
        Guid videoId,
        CancellationToken cancellationToken = default)
    {
        var video = await _videoRepository.GetByIdAsync(
            videoId,
            cancellationToken);

        if (video is null)
            return null;

        return new ProgramVideoDto
        {
            Id = video.Id,
            Title = video.Title,
            VideoUrl = video.VideoStorage.FileKey,
            Duration = video.Duration,
            Order = video.Order
        };
    }

    public async Task SaveProgressAsync(
        Guid userId,
        Guid videoId,
        int currentPositionSeconds,
        CancellationToken cancellationToken = default)
    {
        var progress = await _progressRepository.GetAsync(
            userId,
            videoId,
            cancellationToken);

        if (progress is null)
        {
            progress = new UserVideoProgress(
                userId,
                videoId);

            progress.UpdateProgress(currentPositionSeconds);

            await _progressRepository.AddAsync(
                progress,
                cancellationToken);
        }
        else
        {
            progress.UpdateProgress(currentPositionSeconds);
        }

        await _progressRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task CompleteVideoAsync(
        Guid userId,
        Guid videoId,
        CancellationToken cancellationToken = default)
    {
        var progress = await _progressRepository.GetAsync(
            userId,
            videoId,
            cancellationToken);

        if (progress is null)
        {
            progress = new UserVideoProgress(
                userId,
                videoId);

            progress.MarkCompleted();

            await _progressRepository.AddAsync(
                progress,
                cancellationToken);
        }
        else
        {
            progress.MarkCompleted();
        }

        await _progressRepository.SaveChangesAsync(cancellationToken);
    }
}