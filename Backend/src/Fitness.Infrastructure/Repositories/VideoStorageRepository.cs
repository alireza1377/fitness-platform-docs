using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class VideoStorageRepository : IVideoStorageRepository
{
    private readonly FitnessDbContext _context;

    public VideoStorageRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<VideoStorage?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.VideoStorages
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<VideoStorage?> GetByFileKeyAsync(
        string fileKey,
        CancellationToken cancellationToken = default)
    {
        return await _context.VideoStorages
            .FirstOrDefaultAsync(
                x => x.FileKey == fileKey,
                cancellationToken);
    }

    public async Task<List<VideoStorage>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.VideoStorages
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        VideoStorage entity,
        CancellationToken cancellationToken = default)
    {
        await _context.VideoStorages.AddAsync(
            entity,
            cancellationToken);
    }

    public Task UpdateAsync(
        VideoStorage entity,
        CancellationToken cancellationToken = default)
    {
        _context.VideoStorages.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        VideoStorage entity,
        CancellationToken cancellationToken = default)
    {
        _context.VideoStorages.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}