namespace Fitness.Application.DTOs.Programs;

public class FitnessProgramResponse
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? CoverImageUrl { get; set; }

    public int TotalVideos { get; set; }
}