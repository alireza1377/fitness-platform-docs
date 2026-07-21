using System;

namespace Fitness.Application.Interfaces;

public interface IStatisticsService
{
    Task AddWorkoutAsync(
        Guid userId,
        int workoutMinutes,
        bool videoCompleted,
        bool programCompleted,
        CancellationToken cancellationToken = default);
}