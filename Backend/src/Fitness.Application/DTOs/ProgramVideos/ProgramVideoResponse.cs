using Fitness.Domain.Enums;

namespace Fitness.Application.DTOs.ProgramVideos;

public class ProgramVideoResponse
{
    public Guid Id { get; set; }

    public Guid FitnessProgramId { get; set; }

    public Guid VideoStorageId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Order { get; set; }

    public TimeSpan Duration { get; set; }

    public string? ThumbnailUrl { get; set; }

    public VideoDifficulty Difficulty { get; set; }

    public int EstimatedCalories { get; set; }

    public bool IsFree { get; set; }

    public bool IsPublished { get; set; }

    public bool HasSubtitle { get; set; }

    public bool HasMultiAudio { get; set; }
}