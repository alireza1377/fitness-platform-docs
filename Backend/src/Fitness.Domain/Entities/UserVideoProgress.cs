using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class UserVideoProgress : AuditableEntity
{
    public Guid UserId { get; private set; }

    public Guid ProgramVideoId { get; private set; }

    public int CurrentPositionSeconds { get; private set; }

    public bool Completed { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    public DateTime LastWatchAt { get; private set; }

    public User User { get; private set; } = null!;

    public ProgramVideo ProgramVideo { get; private set; } = null!;

    private UserVideoProgress()
    {
    }

    public UserVideoProgress(
        Guid userId,
        Guid programVideoId)
    {
        UserId = userId;
        ProgramVideoId = programVideoId;

        CurrentPositionSeconds = 0;
        Completed = false;
        LastWatchAt = DateTime.UtcNow;
    }

    public void UpdateProgress(int currentPositionSeconds)
    {
        CurrentPositionSeconds = currentPositionSeconds;
        LastWatchAt = DateTime.UtcNow;

        SetUpdated();
    }

   public bool MarkCompleted()
{
    if (Completed)
        return false;

    Completed = true;
    CompletedAt = DateTime.UtcNow;
    LastWatchAt = DateTime.UtcNow;

    SetUpdated();

    return true;
}
}