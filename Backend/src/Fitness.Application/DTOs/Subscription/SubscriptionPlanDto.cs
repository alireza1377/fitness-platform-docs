namespace Fitness.Application.DTOs.Subscription;

public class SubscriptionPlanDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationDays { get; set; }

    public string Type { get; set; } = string.Empty;
}