namespace Fitness.Infrastructure.Configuration;

public class FFmpegOptions
{
    public const string SectionName = "FFmpeg";

    public string FFprobePath { get; set; } = string.Empty;

    public string FFmpegPath { get; set; } = string.Empty;
}