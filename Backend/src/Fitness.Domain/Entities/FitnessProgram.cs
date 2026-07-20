using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class FitnessProgram : AuditableEntity
{
    public Guid CategoryId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string? CoverImageUrl { get; private set; }

    public int TotalVideos { get; private set; }

    public Category Category { get; private set; } = null!;

    public ICollection<ProgramVideo> Videos { get; private set; }
        = new List<ProgramVideo>();

    private FitnessProgram()
    {
    }

    public FitnessProgram(
        Guid categoryId,
        string title,
        string? description = null,
        string? coverImageUrl = null)
    {
        CategoryId = categoryId;
        Title = title;
        Description = description;
        CoverImageUrl = coverImageUrl;
    }

    public void Update(
        string title,
        string? description,
        string? coverImageUrl)
    {
        Title = title;
        Description = description;
        CoverImageUrl = coverImageUrl;

        SetUpdated();
    }

    public void UpdateVideoCount(int count)
    {
        TotalVideos = count;

        SetUpdated();
    }
}