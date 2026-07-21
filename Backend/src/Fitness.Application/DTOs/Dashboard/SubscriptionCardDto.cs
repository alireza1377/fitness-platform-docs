namespace Fitness.Application.DTOs.Dashboard;

public class SubscriptionCardDto
{
    public bool IsPremium { get; set; }

    public DateTime? ExpireAt { get; set; }

    public int RemainingDays { get; set; }
}