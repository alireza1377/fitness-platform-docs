using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IUserStatisticsRepository _statisticsRepository;

    public StatisticsService(
        IUserStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task AddWorkoutAsync(
        Guid userId,
        int workoutMinutes,
        bool videoCompleted,
        bool programCompleted,
        CancellationToken cancellationToken = default)
    {
        var statistics =
            await _statisticsRepository.GetByUserAsync(
                userId,
                cancellationToken);

        if (statistics is null)
        {
            statistics = new UserStatistics(userId);

            await _statisticsRepository.AddAsync(
                statistics,
                cancellationToken);
        }

        statistics.AddWorkout(
            workoutMinutes,
            programCompleted,
            videoCompleted,
            DateOnly.FromDateTime(DateTime.UtcNow));

        await _statisticsRepository.SaveChangesAsync(
            cancellationToken);
    }
}