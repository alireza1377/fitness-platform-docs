using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class SubscriptionPlan : AuditableEntity
{
    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public SubscriptionType Type { get; private set; }

    public int DurationDays { get; private set; }

    public bool IsActive { get; private set; }

    public int DisplayOrder { get; private set; }

    private SubscriptionPlan()
    {
    }

    public SubscriptionPlan(
        string title,
        string description,
        decimal price,
        SubscriptionType type,
        int durationDays,
        int displayOrder)
    {
        Title = title;
        Description = description;
        Price = price;
        Type = type;
        DurationDays = durationDays;
        DisplayOrder = displayOrder;
        IsActive = true;
    }

    public void Update(
        string title,
        string description,
        decimal price,
        int durationDays,
        int displayOrder)
    {
        Title = title;
        Description = description;
        Price = price;
        DurationDays = durationDays;
        DisplayOrder = displayOrder;

        SetUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdated();
    }
}