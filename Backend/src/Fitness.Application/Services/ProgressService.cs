using Fitness.Application.DTOs.Progress;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class ProgressService : IProgressService
{
    private readonly IProgramVideoRepository _videoRepository;
    private readonly IUserVideoProgressRepository _videoProgressRepository;
    private readonly IUserProgramProgressRepository _programProgressRepository;

    public ProgressService(
        IProgramVideoRepository videoRepository,
        IUserVideoProgressRepository videoProgressRepository,
        IUserProgramProgressRepository programProgressRepository)
    {
        _videoRepository = videoRepository;
        _videoProgressRepository = videoProgressRepository;
        _programProgressRepository = programProgressRepository;
    }

    public async Task<ProgramProgressDto> GetProgressAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        var totalVideos =
            await _videoRepository.CountAsync(
                programId,
                cancellationToken);

        var completedVideos =
            await _videoProgressRepository.CountCompletedVideosAsync(
                userId,
                programId,
                cancellationToken);

        double percentage = 0;

        if (totalVideos > 0)
        {
            percentage =
                Math.Round(
                    (double)completedVideos / totalVideos * 100,
                    2);
        }

        return new ProgramProgressDto
        {
            ProgramId = programId,
            TotalVideos = totalVideos,
            CompletedVideos = completedVideos,
            Percentage = percentage,
            IsCompleted = percentage >= 100
        };
    }

    public async Task CompleteProgramAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        var progress =
            await _programProgressRepository.GetAsync(
                userId,
                programId,
                cancellationToken);

        if (progress is null)
        {
           var totalVideos = await _videoRepository.CountAsync(
    programId,
    cancellationToken);

progress = new UserProgramProgress(
    userId,
    programId,
    totalVideos);

            progress.MarkCompleted();

            await _programProgressRepository.AddAsync(
                progress,
                cancellationToken);
        }
        else
        {
            progress.MarkCompleted();

            await _programProgressRepository.UpdateAsync(
                progress,
                cancellationToken);
        }

        await _programProgressRepository.SaveChangesAsync(
            cancellationToken);
    }
}