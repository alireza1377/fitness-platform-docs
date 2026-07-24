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

    public async Task AddAsync(
        FitnessProgram program,
        CancellationToken cancellationToken = default)
    {
        await _context.FitnessPrograms.AddAsync(
            program,
            cancellationToken);
    }

    public async Task<List<FitnessProgram>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.FitnessPrograms
            .Include(x => x.Category)
            .Include(x => x.Videos)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<FitnessProgram>> GetByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.FitnessPrograms
            .Where(x => x.CategoryId == categoryId)
            .Include(x => x.Category)
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

    public void Remove(FitnessProgram program)
    {
        _context.FitnessPrograms.Remove(program);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ReorderAsync(
    Guid categoryId,
    IReadOnlyList<Guid> programIds,
    CancellationToken cancellationToken = default)
{
    var programs = await _context.FitnessPrograms
        .Where(x => x.CategoryId == categoryId)
        .ToListAsync(cancellationToken);

    for (int i = 0; i < programIds.Count; i++)
    {
        var program = programs.FirstOrDefault(x => x.Id == programIds[i]);

        if (program != null)
            program.SetDisplayOrder(i + 1);
    }
}
}