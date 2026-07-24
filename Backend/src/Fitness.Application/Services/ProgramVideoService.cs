using Fitness.Application.DTOs.ProgramVideos;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class ProgramVideoService : IProgramVideoService
{
    private readonly IProgramVideoRepository _repository;

    public ProgramVideoService(
        IProgramVideoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProgramVideoResponse>> GetByProgramAsync(
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        var videos = await _repository.GetByProgramAsync(
            programId,
            cancellationToken);

        return videos.Select(Map).ToList();
    }

    public async Task<ProgramVideoResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var video = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        return video == null ? null : Map(video);
    }

    public async Task<Guid> CreateAsync(
        CreateProgramVideoRequest request,
        CancellationToken cancellationToken = default)
    {
        var video = new ProgramVideo(
            request.FitnessProgramId,
            request.Order,
            request.Title,
            request.VideoStorageId,
            request.Description,
            null,
            TimeSpan.Zero,
            null,
            request.Difficulty,
            request.EstimatedCalories,
            request.IsFree,
            request.IsPublished,
            request.HasSubtitle,
            request.HasMultiAudio);

        await _repository.AddAsync(
            video,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return video.Id;
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateProgramVideoRequest request,
        CancellationToken cancellationToken = default)
    {
        var video = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (video == null)
            throw new Exception("Video not found.");

        video.Update(
            request.Title,
            request.Description,
            request.VideoStorageId,
            null,
            video.Duration,
            video.ThumbnailUrl,
            request.Difficulty,
            request.EstimatedCalories,
            request.IsFree,
            request.IsPublished,
            request.HasSubtitle,
            request.HasMultiAudio);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var video = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (video == null)
            return;

        _repository.Remove(video);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    private static ProgramVideoResponse Map(
        ProgramVideo video)
    {
        return new ProgramVideoResponse
        {
            Id = video.Id,
            FitnessProgramId = video.FitnessProgramId,
            VideoStorageId = video.VideoStorageId,
            Title = video.Title,
            Description = video.Description,
            Order = video.Order,
            Duration = video.Duration,
            ThumbnailUrl = video.ThumbnailUrl,
            Difficulty = video.Difficulty,
            EstimatedCalories = video.EstimatedCalories,
            IsFree = video.IsFree,
            IsPublished = video.IsPublished,
            HasSubtitle = video.HasSubtitle,
            HasMultiAudio = video.HasMultiAudio
        };
    }

    public async Task ReorderAsync(
    ReorderProgramVideosRequest request,
    CancellationToken cancellationToken = default)
{
    await _repository.ReorderAsync(
        request.ProgramId,
        request.VideoIds,
        cancellationToken);

    await _repository.SaveChangesAsync(
        cancellationToken);
}
}