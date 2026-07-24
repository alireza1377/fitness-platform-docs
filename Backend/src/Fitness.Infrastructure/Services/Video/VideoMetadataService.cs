using System.Diagnostics;
using System.Text.Json;
using Fitness.Application.DTOs.Videos;
using Fitness.Application.Interfaces;
using Fitness.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Fitness.Infrastructure.Services.Video;

public class VideoMetadataService : IVideoMetadataService
{
    public VideoMetadataService(
    IOptions<FFmpegOptions> options)
{
    _options = options.Value;
}
    private readonly FFmpegOptions _options;    public async Task<VideoMetadataResult> ExtractAsync(

        string filePath,
        CancellationToken cancellationToken = default)
    {
        var process = new Process();

       process.StartInfo.FileName = _options.FFprobePath;

        process.StartInfo.Arguments =
            $"-v quiet -print_format json -show_streams \"{filePath}\"";

        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardError = true;

        process.Start();

        var json = await process.StandardOutput.ReadToEndAsync();

        await process.WaitForExitAsync(cancellationToken);

        using var document = JsonDocument.Parse(json);

        var stream = document.RootElement
            .GetProperty("streams")
            .EnumerateArray()
            .First(x => x.GetProperty("codec_type").GetString() == "video");

        return new VideoMetadataResult
        {
            Width = stream.GetProperty("width").GetInt32(),

            Height = stream.GetProperty("height").GetInt32(),

            DurationSeconds = (int)Math.Round(
                double.Parse(stream.GetProperty("duration").GetString()!)),

            Bitrate = int.Parse(
                stream.GetProperty("bit_rate").GetString()!)
        };
    }
}