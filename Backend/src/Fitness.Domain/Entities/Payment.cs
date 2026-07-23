using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class Payment : AuditableEntity
{
    public Guid UserId { get; private set; }

    public decimal Amount { get; private set; }

    public PaymentGateway Gateway { get; private set; }

    public PaymentStatus Status { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public string? Authority { get; private set; }

    public string? RefId { get; private set; }

    public string? TrackingCode { get; private set; }

    public DateTime? PaidAt { get; private set; }

    public User User { get; private set; } = null!;

    private Payment()
    {
    }

   public Payment(
    Guid userId,
    Guid subscriptionPlanId,
    decimal amount,
    PaymentGateway gateway,
    string description)
{
    UserId = userId;
    SubscriptionPlanId = subscriptionPlanId;

    Amount = amount;
    Gateway = gateway;
    Description = description;

    Status = PaymentStatus.Pending;
}
    public void SetAuthority(string authority)
    {
        Authority = authority;
        SetUpdated();
    }

    public void MarkSucceeded(
        string authority,
        string refId,
        string trackingCode)
    {
        Authority = authority;
        RefId = refId;
        TrackingCode = trackingCode;

        Status = PaymentStatus.Succeeded;
        PaidAt = DateTime.UtcNow;

        SetUpdated();
    }

    public void MarkFailed()
    {
        Status = PaymentStatus.Failed;
        SetUpdated();
    }

    public void Cancel()
    {
        Status = PaymentStatus.Cancelled;
        SetUpdated();
    }
    public Guid SubscriptionPlanId { get; private set; }

    public SubscriptionPlan SubscriptionPlan { get; private set; } = null!;
}