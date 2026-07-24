using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class ProgramVideoRepository : IProgramVideoRepository
{
    private readonly FitnessDbContext _context;

    public ProgramVideoRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<ProgramVideo?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
            .Include(x => x.VideoStorage)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<List<ProgramVideo>> GetByProgramAsync(
        Guid fitnessProgramId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
            .Include(x => x.VideoStorage)
            .Where(x => x.FitnessProgramId == fitnessProgramId)
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
            .CountAsync(
                x => x.FitnessProgramId == programId,
                cancellationToken);
    }

    public async Task<Guid?> GetProgramIdByVideoIdAsync(
        Guid videoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
            .Where(x => x.Id == videoId)
            .Select(x => (Guid?)x.FitnessProgramId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetDurationInMinutesAsync(
        Guid videoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
            .Where(x => x.Id == videoId)
            .Select(x => (int)Math.Ceiling(x.Duration.TotalMinutes))
            .FirstAsync(cancellationToken);
    }

    public async Task<ProgramVideo?> GetWithProgramAsync(
        Guid videoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
            .Include(x => x.VideoStorage)
            .Include(x => x.FitnessProgram)
            .FirstOrDefaultAsync(
                x => x.Id == videoId,
                cancellationToken);
    }

    public async Task<ProgramVideo?> GetNextVideoAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
            .Include(x => x.VideoStorage)
            .Include(x => x.Progresses)
            .Where(x => x.FitnessProgramId == programId)
            .OrderBy(x => x.Order)
            .FirstOrDefaultAsync(
                x =>
                    !x.Progresses.Any(p =>
                        p.UserId == userId &&
                        p.Completed),
                cancellationToken);
    }

    public async Task AddAsync(
    ProgramVideo video,
    CancellationToken cancellationToken = default)
{
    await _context.ProgramVideos.AddAsync(
        video,
        cancellationToken);
}

public void Remove(
    ProgramVideo video)
{
    _context.ProgramVideos.Remove(video);
}

public async Task SaveChangesAsync(
    CancellationToken cancellationToken = default)
{
    await _context.SaveChangesAsync(cancellationToken);
}

public async Task ReorderAsync(
    Guid programId,
    IReadOnlyList<Guid> videoIds,
    CancellationToken cancellationToken = default)
{
    var videos = await _context.ProgramVideos
        .Where(x => x.FitnessProgramId == programId)
        .ToListAsync(cancellationToken);

    for (var i = 0; i < videoIds.Count; i++)
    {
        var video = videos.FirstOrDefault(x => x.Id == videoIds[i]);

        if (video != null)
        {
            video.SetOrder(i + 1);
        }
    }
}
}