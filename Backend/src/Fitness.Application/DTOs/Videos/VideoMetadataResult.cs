namespace Fitness.Application.DTOs.Videos;

public sealed class VideoMetadataResult
{
    public int DurationSeconds { get; init; }

    public int Width { get; init; }

    public int Height { get; init; }

    public int Bitrate { get; init; }
}