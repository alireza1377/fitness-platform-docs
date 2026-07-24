using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class ProgramVideo : AuditableEntity
{
    public Guid FitnessProgramId { get; private set; }

    public Guid VideoStorageId { get; private set; }

    public int Order { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string? DownloadUrl { get; private set; }

    public TimeSpan Duration { get; private set; }

    public string? ThumbnailUrl { get; private set; }

    public VideoDifficulty Difficulty { get; private set; }

    public int EstimatedCalories { get; private set; }

    public bool IsFree { get; private set; }

    public bool IsPublished { get; private set; }

    public bool HasSubtitle { get; private set; }

    public bool HasMultiAudio { get; private set; }

    public FitnessProgram FitnessProgram { get; private set; } = null!;

    public VideoStorage VideoStorage { get; private set; } = null!;

    public ICollection<UserVideoProgress> Progresses { get; private set; }
        = new List<UserVideoProgress>();

    public ICollection<VideoSubtitle> Subtitles { get; private set; }
        = new List<VideoSubtitle>();

    public ICollection<VideoAudioTrack> AudioTracks { get; private set; }
        = new List<VideoAudioTrack>();

    private ProgramVideo()
    {
    }

    public ProgramVideo(
        Guid fitnessProgramId,
        int order,
        string title,
        Guid videoStorageId,
        string? description,
        string? downloadUrl,
        TimeSpan duration,
        string? thumbnailUrl,
        VideoDifficulty difficulty,
        int estimatedCalories,
        bool isFree,
        bool isPublished,
        bool hasSubtitle,
        bool hasMultiAudio)
    {
        FitnessProgramId = fitnessProgramId;
        Order = order;
        Title = title;
        VideoStorageId = videoStorageId;
        Description = description;
        DownloadUrl = downloadUrl;
        Duration = duration;
        ThumbnailUrl = thumbnailUrl;
        Difficulty = difficulty;
        EstimatedCalories = estimatedCalories;
        IsFree = isFree;
        IsPublished = isPublished;
        HasSubtitle = hasSubtitle;
        HasMultiAudio = hasMultiAudio;
    }

    public void Update(
        string title,
        string? description,
        Guid videoStorageId,
        string? downloadUrl,
        TimeSpan duration,
        string? thumbnailUrl,
        VideoDifficulty difficulty,
        int estimatedCalories,
        bool isFree,
        bool isPublished,
        bool hasSubtitle,
        bool hasMultiAudio)
    {
        Title = title;
        Description = description;
        VideoStorageId = videoStorageId;
        DownloadUrl = downloadUrl;
        Duration = duration;
        ThumbnailUrl = thumbnailUrl;
        Difficulty = difficulty;
        EstimatedCalories = estimatedCalories;
        IsFree = isFree;
        IsPublished = isPublished;
        HasSubtitle = hasSubtitle;
        HasMultiAudio = hasMultiAudio;

        SetUpdated();
    }

    public void SetOrder(int order)
{
    Order = order;
    SetUpdated();
}
}