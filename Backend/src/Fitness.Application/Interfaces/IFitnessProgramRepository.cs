using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IFitnessProgramRepository
{
    Task AddAsync(
        FitnessProgram program,
        CancellationToken cancellationToken = default);

    Task<List<FitnessProgram>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<List<FitnessProgram>> GetByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<FitnessProgram?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    void Remove(FitnessProgram program);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);

        Task ReorderAsync(
    Guid categoryId,
    IReadOnlyList<Guid> programIds,
    CancellationToken cancellationToken = default);
}