using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class VideoAudioTrack : AuditableEntity
{
    public Guid ProgramVideoId { get; private set; }

    public string Language { get; private set; } = string.Empty;

    public string AudioUrl { get; private set; } = string.Empty;

    public bool IsDefault { get; private set; }

    public ProgramVideo ProgramVideo { get; private set; } = null!;

    private VideoAudioTrack()
    {
    }

    public VideoAudioTrack(
        Guid programVideoId,
        string language,
        string audioUrl,
        bool isDefault = false)
    {
        ProgramVideoId = programVideoId;
        Language = language;
        AudioUrl = audioUrl;
        IsDefault = isDefault;
    }
}