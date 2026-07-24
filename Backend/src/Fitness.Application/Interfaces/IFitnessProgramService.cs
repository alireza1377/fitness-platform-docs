using Fitness.Application.DTOs.Programs;

namespace Fitness.Application.Interfaces;

public interface IFitnessProgramService
{
    Task<Guid> CreateAsync(
        CreateFitnessProgramRequest request,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Guid id,
        UpdateFitnessProgramRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<FitnessProgramResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FitnessProgramResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FitnessProgramResponse>> GetByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

        Task ReorderAsync(
    ReorderProgramsRequest request,
    CancellationToken cancellationToken = default);

    Task UploadCoverAsync(
    Guid programId,
    Stream stream,
    string fileName,
    CancellationToken cancellationToken = default);
}