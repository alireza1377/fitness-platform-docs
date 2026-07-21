using Fitness.Application.DTOs.Progress;

namespace Fitness.Application.Interfaces;

public interface IProgressService
{
    Task<ProgramProgressDto> GetProgressAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken = default);

    Task CompleteProgramAsync(
        Guid userId,
        Guid programId,
        CancellationToken cancellationToken = default);
}