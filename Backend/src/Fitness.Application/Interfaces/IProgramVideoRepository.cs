using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IProgramVideoRepository
{
    Task<List<ProgramVideo>> GetByProgramAsync(
        Guid fitnessProgramId,
        CancellationToken cancellationToken = default);

    Task<ProgramVideo?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<int> CountAsync(
        Guid programId,
        CancellationToken cancellationToken = default);

        Task<Guid?> GetProgramIdByVideoIdAsync(
    Guid videoId,
    CancellationToken cancellationToken = default);

    Task<int> GetDurationInMinutesAsync(
    Guid videoId,
    CancellationToken cancellationToken = default);
    Task<ProgramVideo?> GetWithProgramAsync(
    Guid videoId,
    CancellationToken cancellationToken = default);

    Task<ProgramVideo?> GetNextVideoAsync(
    Guid userId,
    Guid programId,
    CancellationToken cancellationToken = default);

    Task AddAsync(
    ProgramVideo video,
    CancellationToken cancellationToken = default);

void Remove(
    ProgramVideo video);

Task SaveChangesAsync(
    CancellationToken cancellationToken = default);

    Task ReorderAsync(
    Guid programId,
    IReadOnlyList<Guid> videoIds,
    CancellationToken cancellationToken = default);
}