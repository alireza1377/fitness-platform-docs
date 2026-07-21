using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class ProgramVideo : AuditableEntity
{
    public Guid FitnessProgramId { get; private set; }

    public int Order { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string VideoUrl { get; private set; } = string.Empty;

    public TimeSpan Duration { get; private set; }

    public string? ThumbnailUrl { get; private set; }

    public FitnessProgram FitnessProgram { get; private set; } = null!;

public ICollection<UserVideoProgress> Progresses { get; private set; }
    = new List<UserVideoProgress>();
    private ProgramVideo()
    {
    }

    public ProgramVideo(
        Guid fitnessProgramId,
        int order,
        string title,
        string videoUrl,
        TimeSpan duration,
        string? thumbnailUrl = null)
    {
        FitnessProgramId = fitnessProgramId;
        Order = order;
        Title = title;
        VideoUrl = videoUrl;
        Duration = duration;
        ThumbnailUrl = thumbnailUrl;
    }

    public void Update(
        string title,
        string videoUrl,
        TimeSpan duration,
        string? thumbnailUrl)
    {
        Title = title;
        VideoUrl = videoUrl;
        Duration = duration;
        ThumbnailUrl = thumbnailUrl;

        SetUpdated();
    }
}