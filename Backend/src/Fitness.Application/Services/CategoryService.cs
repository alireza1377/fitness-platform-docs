using Fitness.Application.DTOs.Categories;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(
        ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = new Category(
            request.Name,
            request.Slug,
            request.Description,
            request.ImageUrl,
            request.DisplayOrder);

        await _repository.AddAsync(
            category,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return category.Id;
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (category == null)
            throw new Exception("Category not found.");

        category.Update(
            request.Name,
            request.Slug,
            request.Description,
            request.ImageUrl,
            request.DisplayOrder);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (category == null)
            throw new Exception("Category not found.");

        _repository.Remove(category);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<CategoryResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (category == null)
            return null;

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            ImageUrl = category.ImageUrl,
            DisplayOrder = category.DisplayOrder,
            IsPublished = category.IsPublished
        };
    }

    public async Task<IReadOnlyList<CategoryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _repository.GetAllAsync(
            cancellationToken);

        return categories
            .Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                DisplayOrder = x.DisplayOrder,
                IsPublished = x.IsPublished
            })
            .ToList();
    }
}