using Fitness.Domain.Enums;

namespace Fitness.Application.DTOs.ProgramVideos;

public class UpdateProgramVideoRequest
{
    public Guid VideoStorageId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Order { get; set; }

    public VideoDifficulty Difficulty { get; set; }

    public int EstimatedCalories { get; set; }

    public bool IsFree { get; set; }

    public bool IsPublished { get; set; }

    public bool HasSubtitle { get; set; }

    public bool HasMultiAudio { get; set; }
}