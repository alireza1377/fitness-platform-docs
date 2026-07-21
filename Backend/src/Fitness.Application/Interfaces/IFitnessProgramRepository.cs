using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IFitnessProgramRepository
{
    Task<List<FitnessProgram>> GetByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<FitnessProgram?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

        Task<List<FitnessProgram>> GetAllAsync(
    CancellationToken cancellationToken = default);
}