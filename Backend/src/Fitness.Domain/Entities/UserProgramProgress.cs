using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class UserProgramProgress : AuditableEntity
{
    public Guid UserId { get; private set; }

    public Guid FitnessProgramId { get; private set; }

    public int CompletedVideos { get; private set; }

    public int TotalVideos { get; private set; }

    public double Percentage { get; private set; }

    public bool IsCompleted { get; private set; }

    private UserProgramProgress()
    {
    }

    public UserProgramProgress(
        Guid userId,
        Guid fitnessProgramId,
        int totalVideos)
    {
        UserId = userId;
        FitnessProgramId = fitnessProgramId;
        TotalVideos = totalVideos;
    }

    public void Update(int completedVideos)
    {
        CompletedVideos = completedVideos;

        Percentage =
            TotalVideos == 0
                ? 0
                : completedVideos * 100.0 / TotalVideos;

        IsCompleted =
            completedVideos >= TotalVideos;

        SetUpdated();
    }
}