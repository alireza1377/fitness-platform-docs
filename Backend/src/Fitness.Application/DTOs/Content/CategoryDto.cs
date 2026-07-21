namespace Fitness.Application.DTOs.Content;

public class CategoryDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public int ProgramsCount { get; set; }
}