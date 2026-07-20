using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IProgramVideoRepository
{
    Task<List<ProgramVideo>> GetByProgramAsync(
        Guid fitnessProgramId,
        CancellationToken cancellationToken = default);
}