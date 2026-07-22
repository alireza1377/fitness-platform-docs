using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Dashboard.Builders;

public class WorkoutBuilder
{
    private readonly IUserProgramProgressRepository _programProgressRepository;
    private readonly IProgramVideoRepository _videoRepository;

    public WorkoutBuilder(
        IUserProgramProgressRepository programProgressRepository,
        IProgramVideoRepository videoRepository)
    {
        _programProgressRepository = programProgressRepository;
        _videoRepository = videoRepository;
    }

    public async Task<TodayWorkoutCardDto?> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var currentProgram =
            await _programProgressRepository.GetCurrentProgramAsync(
                userId,
                cancellationToken);

        if (currentProgram is null)
            return null;

        var nextVideo =
            await _videoRepository.GetNextVideoAsync(
                userId,
                currentProgram.FitnessProgramId,
                cancellationToken);

        if (nextVideo is null)
            return null;

        return new TodayWorkoutCardDto
        {
            ProgramId = currentProgram.FitnessProgramId,
            ProgramTitle = nextVideo.FitnessProgram.Title,
            VideoTitle = nextVideo.Title,
            DayNumber = nextVideo.Order,
            EstimatedMinutes =
                (int)Math.Ceiling(nextVideo.Duration.TotalMinutes)
        };
    }
}