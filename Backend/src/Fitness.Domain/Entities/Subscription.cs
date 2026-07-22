using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class Subscription : AuditableEntity
{
    public Guid UserId { get; private set; }

    public SubscriptionType PlanType { get; private set; }

    public SubscriptionStatus Status { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public bool IsAutoRenew { get; private set; }

    public string? PaymentReference { get; private set; }

    public User User { get; private set; } = null!;

    private Subscription()
    {
    }

    public Subscription(
        Guid userId,
        SubscriptionType planType,
        DateTime startDate,
        DateTime endDate,
        bool isAutoRenew = false,
        string? paymentReference = null)
    {
        UserId = userId;

        PlanType = planType;

        StartDate = startDate;

        EndDate = endDate;

        IsAutoRenew = isAutoRenew;

        PaymentReference = paymentReference;

        Status = SubscriptionStatus.Active;
    }

    public bool IsActive()
    {
        return Status == SubscriptionStatus.Active &&
               EndDate > DateTime.UtcNow;
    }

    public int RemainingDays()
    {
        if (!IsActive())
            return 0;

        return (EndDate - DateTime.UtcNow).Days;
    }

    public void Expire()
    {
        Status = SubscriptionStatus.Expired;

        SetUpdated();
    }

    public void Cancel()
    {
        Status = SubscriptionStatus.Cancelled;

        SetUpdated();
    }

    public void Renew(
        DateTime newEndDate,
        bool autoRenew)
    {
        EndDate = newEndDate;

        IsAutoRenew = autoRenew;

        Status = SubscriptionStatus.Active;

        SetUpdated();
    }
}