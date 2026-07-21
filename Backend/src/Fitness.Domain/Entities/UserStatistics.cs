using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class UserStatistics : AuditableEntity
{
    public Guid UserId { get; private set; }

    public int WorkoutMinutes { get; private set; }

    public int CompletedPrograms { get; private set; }

    public int CompletedVideos { get; private set; }

    public int CurrentStreak { get; private set; }

    public int BestStreak { get; private set; }

    public DateOnly? LastWorkoutDate { get; private set; }

    public User User { get; private set; } = null!;

    private UserStatistics()
    {
    }

    public UserStatistics(Guid userId)
    {
        UserId = userId;
    }

    public void AddWorkout(
        int workoutMinutes,
        bool programCompleted,
        bool videoCompleted,
        DateOnly workoutDate)
    {
        WorkoutMinutes += workoutMinutes;

        if (videoCompleted)
            CompletedVideos++;

        if (programCompleted)
            CompletedPrograms++;

        UpdateStreak(workoutDate);

        SetUpdated();
    }

    private void UpdateStreak(DateOnly workoutDate)
    {
        if (LastWorkoutDate == null)
        {
            CurrentStreak = 1;
            BestStreak = 1;
        }
        else
        {
            var diff = workoutDate.DayNumber - LastWorkoutDate.Value.DayNumber;

            if (diff == 1)
            {
                CurrentStreak++;
            }
            else if (diff > 1)
            {
                CurrentStreak = 1;
            }

            if (CurrentStreak > BestStreak)
                BestStreak = CurrentStreak;
        }

        LastWorkoutDate = workoutDate;
    }
}