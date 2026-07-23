// =========================
// CategoryRepository.cs
// =========================

using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly FitnessDbContext _context;

    public CategoryRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
}