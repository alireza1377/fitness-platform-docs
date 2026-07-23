using Fitness.Application.DTOs.Content;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class ContentService : IContentService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFitnessProgramRepository _fitnessProgramRepository;
    private readonly IProgramVideoRepository _programVideoRepository;

    public ContentService(
        ICategoryRepository categoryRepository,
        IFitnessProgramRepository fitnessProgramRepository,
        IProgramVideoRepository programVideoRepository)
    {
        _categoryRepository = categoryRepository;
        _fitnessProgramRepository = fitnessProgramRepository;
        _programVideoRepository = programVideoRepository;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        return categories
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Title = x.Name,
                ThumbnailUrl = x.ImageUrl,
                ProgramsCount = x.Programs.Count
            })
            .ToList();
    }

    public async Task<List<FitnessProgramDto>> GetProgramsAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var programs =
            await _fitnessProgramRepository.GetByCategoryAsync(
                categoryId,
                cancellationToken);

        return programs
            .Select(x => new FitnessProgramDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ThumbnailUrl = x.CoverImageUrl,
                VideosCount = x.Videos.Count
            })
            .ToList();
    }

    public async Task<FitnessProgramDetailsDto?> GetProgramAsync(
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        var program =
            await _fitnessProgramRepository.GetByIdAsync(
                programId,
                cancellationToken);

        if (program is null)
            return null;

        return new FitnessProgramDetailsDto
{
    Id = program.Id,
    Title = program.Title,
    Description = program.Description,
    ThumbnailUrl = program.CoverImageUrl,
    CategoryId = program.CategoryId,
    CategoryTitle = program.Category.Name,
    VideosCount = program.Videos.Count
};
    }

    public async Task<List<ProgramVideoDto>> GetVideosAsync(
        Guid programId,
        CancellationToken cancellationToken = default)
    {
        var videos =
            await _programVideoRepository.GetByProgramAsync(
                programId,
                cancellationToken);

        return videos
    .Select(x => new ProgramVideoDto
    {
        Id = x.Id,
        Title = x.Title,
        VideoUrl = x.VideoStorage.FileKey,
        Duration = x.Duration,
        Order = x.Order
    })
    .ToList();
    }
}