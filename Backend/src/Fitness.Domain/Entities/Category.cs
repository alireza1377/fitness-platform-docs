using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class Category : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string? ImageUrl { get; private set; }

    public int DisplayOrder { get; private set; }

    public bool IsPublished { get; private set; }

   public ICollection<FitnessProgram> Programs { get; private set; }
    = new List<FitnessProgram>();

    private Category()
    {
    }

    public Category(
        string name,
        string slug,
        string? description,
        string? imageUrl,
        int displayOrder)
    {
        Name = name;
        Slug = slug;
        Description = description;
        ImageUrl = imageUrl;
        DisplayOrder = displayOrder;

        IsPublished = true;
    }

    public void Update(
        string name,
        string slug,
        string? description,
        string? imageUrl,
        int displayOrder)
    {
        Name = name;
        Slug = slug;
        Description = description;
        ImageUrl = imageUrl;
        DisplayOrder = displayOrder;

        SetUpdated();
    }

    public void Publish()
    {
        IsPublished = true;

        SetUpdated();
    }

    public void UnPublish()
    {
        IsPublished = false;

        SetUpdated();
    }
}