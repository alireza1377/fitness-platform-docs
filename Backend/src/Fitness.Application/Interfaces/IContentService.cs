using Fitness.Application.DTOs.Content;

namespace Fitness.Application.Interfaces;

public interface IContentService
{
    Task<List<CategoryDto>> GetCategoriesAsync(
        CancellationToken cancellationToken = default);

    Task<List<FitnessProgramDto>> GetProgramsAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<FitnessProgramDetailsDto?> GetProgramAsync(
        Guid programId,
        CancellationToken cancellationToken = default);

    Task<List<ProgramVideoDto>> GetVideosAsync(
        Guid programId,
        CancellationToken cancellationToken = default);
}