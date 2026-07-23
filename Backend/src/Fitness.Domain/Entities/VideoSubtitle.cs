using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class VideoSubtitle : AuditableEntity
{
    public Guid ProgramVideoId { get; private set; }

    public string Language { get; private set; } = string.Empty;

    public string SubtitleUrl { get; private set; } = string.Empty;

    public ProgramVideo ProgramVideo { get; private set; } = null!;

    private VideoSubtitle()
    {
    }

    public VideoSubtitle(
        Guid programVideoId,
        string language,
        string subtitleUrl)
    {
        ProgramVideoId = programVideoId;
        Language = language;
        SubtitleUrl = subtitleUrl;
    }
}