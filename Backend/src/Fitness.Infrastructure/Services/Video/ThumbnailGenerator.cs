using System.Diagnostics;
using Fitness.Application.Interfaces;

namespace Fitness.Infrastructure.Services.Video;

public class ThumbnailGenerator : IThumbnailGenerator
{
    public async Task<string> GenerateAsync(
        string videoPath,
        CancellationToken cancellationToken = default)
    {
        var thumbnailsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "storage",
            "thumbnails");

        Directory.CreateDirectory(thumbnailsFolder);

        var thumbnailName =
            $"{Path.GetFileNameWithoutExtension(videoPath)}.jpg";

        var thumbnailPath =
            Path.Combine(thumbnailsFolder, thumbnailName);

        var process = new Process();

        process.StartInfo.FileName = "ffmpeg";

        process.StartInfo.Arguments =
            $"-y -i \"{videoPath}\" -ss 00:00:01 -vframes 1 \"{thumbnailPath}\"";

        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();

        await process.WaitForExitAsync(cancellationToken);

        if (process.ExitCode != 0)
        {
            var error = await process.StandardError.ReadToEndAsync();

            throw new Exception(
                $"Thumbnail generation failed: {error}");
        }

        return thumbnailPath;
    }
}