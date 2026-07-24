using Fitness.Application.DTOs.Programs;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class FitnessProgramService : IFitnessProgramService
{
    private readonly IFitnessProgramRepository _repository;
    private readonly IFileStorageService _fileStorage;

    public FitnessProgramService(
        IFitnessProgramRepository repository,
        IFileStorageService fileStorage)
    {
        _repository = repository;
        _fileStorage = fileStorage;
    }

    public async Task<Guid> CreateAsync(
        CreateFitnessProgramRequest request,
        CancellationToken cancellationToken = default)
    {
        var program = new FitnessProgram(
            request.CategoryId,
            request.Title,
            request.Description,
            request.CoverImageUrl);

        await _repository.AddAsync(
            program,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return program.Id;
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateFitnessProgramRequest request,
        CancellationToken cancellationToken = default)
    {
        var program = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (program == null)
            throw new Exception("Program not found.");

        program.Update(
            request.Title,
            request.Description,
            request.CoverImageUrl);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var program = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (program == null)
            throw new Exception("Program not found.");

        _repository.Remove(program);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<FitnessProgramResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var program = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (program == null)
            return null;

        return Map(program);
    }

    public async Task<IReadOnlyList<FitnessProgramResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var programs = await _repository.GetAllAsync(
            cancellationToken);

        return programs
            .Select(Map)
            .ToList();
    }

    public async Task<IReadOnlyList<FitnessProgramResponse>> GetByCategoryAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var programs = await _repository.GetByCategoryAsync(
            categoryId,
            cancellationToken);

        return programs
            .Select(Map)
            .ToList();
    }

    public async Task ReorderAsync(
        ReorderProgramsRequest request,
        CancellationToken cancellationToken = default)
    {
        await _repository.ReorderAsync(
            request.CategoryId,
            request.ProgramIds,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UploadCoverAsync(
        Guid programId,
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var program = await _repository.GetByIdAsync(
            programId,
            cancellationToken);

        if (program == null)
            throw new Exception("Program not found.");

        var coverPath =
            await _fileStorage.UploadProgramCoverAsync(
                stream,
                fileName,
                cancellationToken);

        program.Update(
            program.Title,
            program.Description,
            coverPath);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    private static FitnessProgramResponse Map(
        FitnessProgram program)
    {
        return new FitnessProgramResponse
        {
            Id = program.Id,
            CategoryId = program.CategoryId,
            CategoryName = program.Category.Name,
            Title = program.Title,
            Description = program.Description,
            CoverImageUrl = program.CoverImageUrl,
            TotalVideos = program.TotalVideos
        };
    }
}