using Fitness.Application.DTOs.Progress;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class ProgressService : IProgressService
{
   private readonly IProgramVideoRepository _videoRepository;
private readonly IUserVideoProgressRepository _videoProgressRepository;
private readonly IUserProgramProgressRepository _programProgressRepository;
private readonly IStatisticsService _statisticsService;

   public ProgressService(
    IProgramVideoRepository videoRepository,
    IUserVideoProgressRepository videoProgressRepository,
    IUserProgramProgressRepository programProgressRepository,
    IStatisticsService statisticsService)
{
    _videoRepository = videoRepository;
    _videoProgressRepository = videoProgressRepository;
    _programProgressRepository = programProgressRepository;
    _statisticsService = statisticsService;
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
            percentage = Math.Round(
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
        var progress = await GetOrCreateProgramProgressAsync(
            userId,
            programId,
            cancellationToken);

        progress.MarkCompleted();

        await _programProgressRepository.UpdateAsync(
            progress,
            cancellationToken);

        await _programProgressRepository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task CompleteVideoAsync(
        Guid userId,
        Guid videoId,
        CancellationToken cancellationToken = default)
    {
        // پیدا کردن ویدئو
        var video = await _videoRepository.GetByIdAsync(
            videoId,
            cancellationToken);

        if (video is null)
            throw new Exception("Video not found.");

        // دریافت یا ایجاد Progress ویدئو
        var progress = await GetOrCreateVideoProgressAsync(
            userId,
            video.Id,
            cancellationToken);

        // اگر قبلاً کامل شده بود، ادامه نده
        if (!progress.MarkCompleted())
            return;

        await _videoProgressRepository.UpdateAsync(
            progress,
            cancellationToken);

        // دریافت یا ایجاد Progress برنامه
        var programProgress = await GetOrCreateProgramProgressAsync(
            userId,
            video.FitnessProgramId,
            cancellationToken);

        // محاسبه تعداد ویدئوهای تکمیل شده
        var completedVideos =
            await _videoProgressRepository.CountCompletedVideosAsync(
                userId,
                video.FitnessProgramId,
                cancellationToken);

        // بروزرسانی درصد پیشرفت برنامه
        programProgress.Update(completedVideos);

        await _programProgressRepository.UpdateAsync(
            programProgress,
            cancellationToken);

        // ذخیره تغییرات
        await _videoProgressRepository.SaveChangesAsync(
            cancellationToken);

        await _programProgressRepository.SaveChangesAsync(
            cancellationToken);

            await _statisticsService.AddWorkoutAsync(
    userId,
    (int)Math.Ceiling(video.Duration.TotalMinutes),
    videoCompleted: true,
    programCompleted: programProgress.IsCompleted,
    cancellationToken);
    }

  public async Task UpdateVideoPositionAsync(
    Guid userId,
    Guid videoId,
    int currentPositionSeconds,
    CancellationToken cancellationToken = default)
{
    var video = await _videoRepository.GetByIdAsync(
        videoId,
        cancellationToken);

    if (video is null)
        throw new Exception("Video not found.");

    var progress = await _videoProgressRepository.GetAsync(
        userId,
        videoId,
        cancellationToken);


    if (progress is null)
    {
        progress = new UserVideoProgress(
            userId,
            videoId);

        progress.UpdateProgress(
            currentPositionSeconds);

        await _videoProgressRepository.AddAsync(
            progress,
            cancellationToken);
    }
    else
    {
        progress.UpdateProgress(
            currentPositionSeconds);

        await _videoProgressRepository.UpdateAsync(
            progress,
            cancellationToken);
    }


    await _videoProgressRepository.SaveChangesAsync(
        cancellationToken);
}

    private async Task<UserProgramProgress> GetOrCreateProgramProgressAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken)
    {
        var progress = await _programProgressRepository.GetAsync(
            userId,
            programId,
            cancellationToken);

        if (progress is not null)
            return progress;

        var totalVideos = await _videoRepository.CountAsync(
            programId,
            cancellationToken);

        progress = new UserProgramProgress(
            userId,
            programId,
            totalVideos);

        await _programProgressRepository.AddAsync(
            progress,
            cancellationToken);

        return progress;
    }

    private async Task<UserVideoProgress> GetOrCreateVideoProgressAsync(
        Guid userId,
        Guid videoId,
        CancellationToken cancellationToken)
    {
        var progress = await _videoProgressRepository.GetAsync(
            userId,
            videoId,
            cancellationToken);

        if (progress is not null)
            return progress;

        progress = new UserVideoProgress(
            userId,
            videoId);

        await _videoProgressRepository.AddAsync(
            progress,
            cancellationToken);

        return progress;
    }
}