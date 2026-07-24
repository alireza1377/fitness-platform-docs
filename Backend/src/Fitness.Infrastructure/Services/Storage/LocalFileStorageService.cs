using System.Security.Cryptography;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Domain.Enums;

namespace Fitness.Infrastructure.Services.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _rootPath;

    public LocalFileStorageService()
    {
        _rootPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "storage");

        Directory.CreateDirectory(_rootPath);
    }

    public async Task<VideoStorage> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(fileName);

        var generatedName = $"{Guid.NewGuid()}{extension}";

        var fullPath = Path.Combine(
            _rootPath,
            generatedName);

        await using (var fileStream = File.Create(fullPath))
        {
            await stream.CopyToAsync(
                fileStream,
                cancellationToken);
        }

        var checksum = await CalculateHashAsync(fullPath);

        var fileInfo = new FileInfo(fullPath);

        var storage = new VideoStorage(
            storageProvider: StorageProvider.LocalStorage,
            bucket: "local",
            region: "localhost",
            fileKey: generatedName,
            originalFileName: fileName,
            contentType: contentType,
            fileSize: fileInfo.Length,
            checksum: checksum,
            durationSeconds: 0,
            width: 0,
            height: 0,
            bitrate: 0,
            thumbnailUrl: null,
            cdnUrl: null);

        storage.MarkReady();

        return storage;
    }

   public async Task<string> UploadThumbnailAsync(
    string videoFileKey,
    Stream stream,
    string fileName,
    CancellationToken cancellationToken = default)
{
    var thumbnailsFolder = Path.Combine(
        Directory.GetCurrentDirectory(),
        "storage",
        "thumbnails");

    Directory.CreateDirectory(thumbnailsFolder);

    var extension = Path.GetExtension(fileName);

    // اسم Thumbnail دقیقاً از اسم ویدئو گرفته می‌شود
    var generatedName =
        $"{Path.GetFileNameWithoutExtension(videoFileKey)}{extension}";

    var fullPath = Path.Combine(
        thumbnailsFolder,
        generatedName);

    await using var fileStream = File.Create(fullPath);

    await stream.CopyToAsync(
        fileStream,
        cancellationToken);

    return generatedName;
}

    public Task DeleteAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetStreamingUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetDownloadUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private static async Task<string> CalculateHashAsync(string path)
    {
        await using var stream = File.OpenRead(path);

        using var sha = SHA256.Create();

        var hash = await sha.ComputeHashAsync(stream);

        return Convert.ToHexString(hash);
    }

    public async Task<string> UploadProgramCoverAsync(
    Stream stream,
    string fileName,
    CancellationToken cancellationToken = default)
{
    var coversFolder = Path.Combine(
        Directory.GetCurrentDirectory(),
        "storage",
        "covers");

    Directory.CreateDirectory(coversFolder);

    var extension = Path.GetExtension(fileName);

    var fileKey = $"{Guid.NewGuid()}{extension}";

    var fullPath = Path.Combine(
        coversFolder,
        fileKey);

    await using var fileStream = File.Create(fullPath);

    await stream.CopyToAsync(
        fileStream,
        cancellationToken);

    return Path.Combine("covers", fileKey)
        .Replace("\\", "/");
}
}