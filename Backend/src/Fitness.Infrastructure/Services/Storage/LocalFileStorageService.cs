using System.Security.Cryptography;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Domain.Enums;

namespace Fitness.Infrastructure.Services.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _rootPath;
    private readonly IVideoStorageRepository _repository;
private readonly IVideoMetadataService _videoMetadataService;
private readonly IThumbnailGenerator _thumbnailGenerator;
    
    public LocalFileStorageService(
    IVideoStorageRepository repository,
    IVideoMetadataService videoMetadataService,
    IThumbnailGenerator thumbnailGenerator)
{
    _repository = repository;
    _videoMetadataService = videoMetadataService;
    _thumbnailGenerator = thumbnailGenerator;

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
        var id = Guid.NewGuid();

        var extension = Path.GetExtension(fileName);

        var key = $"{id}{extension}";

        var path = Path.Combine(_rootPath, key);

        using (var file = File.Create(path))
        {
            await stream.CopyToAsync(file, cancellationToken);
        }

        var hash = await CalculateHashAsync(path);

        var info = new FileInfo(path);

var metadata = await _videoMetadataService.ExtractAsync(
    path,
    cancellationToken);
    var thumbnailPath = await _thumbnailGenerator.GenerateAsync(
    path,
    cancellationToken);
        var storage = new VideoStorage(
            storageProvider: StorageProvider.LocalStorage,
            bucket: "local",
            region: "localhost",
            fileKey: key,
            originalFileName: fileName,
            contentType: contentType,
            fileSize: info.Length,
            checksum: hash,
            durationSeconds: metadata.DurationSeconds,
            width: metadata.Width,
            height: metadata.Height,
            bitrate: metadata.Bitrate,
            thumbnailUrl: thumbnailPath,
            cdnUrl: null);

        storage.MarkReady();

       

        return storage;
    }

    public Task DeleteAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task<string> GetStreamingUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            $"/api/videos/{storageId}/stream");
    }

    public Task<string?> GetDownloadUrlAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<string?>(
            $"/api/videos/{storageId}/download");
    }

    public Task<bool> ExistsAsync(
        Guid storageId,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    private static async Task<string> CalculateHashAsync(string path)
    {
        await using var stream = File.OpenRead(path);

        using var sha = SHA256.Create();

        var hash = await sha.ComputeHashAsync(stream);

        return Convert.ToHexString(hash);
    }
}