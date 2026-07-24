using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface ICategoryRepository
{
    Task AddAsync(
        Category category,
        CancellationToken cancellationToken = default);

    Task<List<Category>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Category?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    void Remove(Category category);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}