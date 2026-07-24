namespace Fitness.Application.DTOs.Programs;

public class UpdateFitnessProgramRequest
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? CoverImageUrl { get; set; }
}