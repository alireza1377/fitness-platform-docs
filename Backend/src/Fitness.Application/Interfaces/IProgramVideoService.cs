using Fitness.Application.DTOs.ProgramVideos;

namespace Fitness.Application.Interfaces;

public interface IProgramVideoService
{
    Task<List<ProgramVideoResponse>> GetByProgramAsync(
        Guid programId,
        CancellationToken cancellationToken = default);

    Task<ProgramVideoResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Guid> CreateAsync(
        CreateProgramVideoRequest request,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Guid id,
        UpdateProgramVideoRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);

        Task ReorderAsync(
    ReorderProgramVideosRequest request,
    CancellationToken cancellationToken = default);
}