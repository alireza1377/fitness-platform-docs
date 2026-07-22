using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Dashboard.Builders;

public class StatisticsBuilder
{
    private readonly IUserStatisticsRepository _statisticsRepository;

    public StatisticsBuilder(
        IUserStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<(StatisticsCardDto?, StreakCardDto?)> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var statistics =
            await _statisticsRepository.GetByUserAsync(
                userId,
                cancellationToken);

        if (statistics is null)
            return (null, null);

        var statisticsCard = new StatisticsCardDto
        {
            CompletedPrograms = statistics.CompletedPrograms,
            CompletedVideos = statistics.CompletedVideos,
            WorkoutMinutes = statistics.WorkoutMinutes,
            CaloriesBurned = 0,
            InProgressPrograms = 0
        };

        var streakCard = new StreakCardDto
        {
            CurrentDays = statistics.CurrentStreak,
            BestDays = statistics.BestStreak
        };

        return (statisticsCard, streakCard);
    }
}