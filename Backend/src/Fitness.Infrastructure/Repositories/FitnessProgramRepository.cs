// =========================
// FitnessProgramRepository.cs
// =========================

using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class FitnessProgramRepository : IFitnessProgramRepository
{
    private readonly FitnessDbContext _context;

    public FitnessProgramRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<List<FitnessProgram>> GetByCategoryAsync(
    Guid categoryId,
    CancellationToken cancellationToken = default)
{
    return await _context.FitnessPrograms
        .Where(x => x.CategoryId == categoryId)
        .Include(x => x.Videos)
        .OrderBy(x => x.Title)
        .ToListAsync(cancellationToken);
}

    public async Task<FitnessProgram?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.FitnessPrograms
            .Include(x => x.Category)
            .Include(x => x.Videos)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
                
    }
    public async Task<List<FitnessProgram>> GetAllAsync(
    CancellationToken cancellationToken = default)
{
    return await _context.FitnessPrograms
        .Include(x => x.Category)
        .Include(x => x.Videos)
        .ToListAsync(cancellationToken);
}
}