using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class Category : AuditableEntity
{
    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string? IconUrl { get; private set; }

    public ICollection<FitnessProgram> Programs { get; private set; }
        = new List<FitnessProgram>();

    private Category()
    {
    }

    public Category(
        string title,
        string? description = null,
        string? iconUrl = null)
    {
        Title = title;
        Description = description;
        IconUrl = iconUrl;
    }

    public void Update(
        string title,
        string? description,
        string? iconUrl)
    {
        Title = title;
        Description = description;
        IconUrl = iconUrl;

        SetUpdated();
    }
}