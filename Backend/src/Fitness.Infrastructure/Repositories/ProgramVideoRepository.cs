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
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<List<ProgramVideo>> GetByProgramAsync(
        Guid fitnessProgramId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProgramVideos
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
}