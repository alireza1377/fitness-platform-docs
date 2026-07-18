using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class AuthIdentity : AuditableEntity
{
    public Guid UserId { get; private set; }

    public bool IsPhoneVerified { get; private set; }

    public bool IsBlocked { get; private set; }

    public int FailedAttempts { get; private set; }

    public DateTime? LastLoginAt { get; private set; }

    // Navigation
    public User User { get; private set; } = null!;

    private AuthIdentity()
    {
    }

    public AuthIdentity(Guid userId)
    {
        UserId = userId;

        IsPhoneVerified = false;
        IsBlocked = false;
        FailedAttempts = 0;
    }

    public void VerifyPhone()
    {
        IsPhoneVerified = true;
        SetUpdated();
    }

    public void LoginSucceeded()
    {
        FailedAttempts = 0;
        LastLoginAt = DateTime.UtcNow;
        SetUpdated();
    }

    public void LoginFailed()
    {
        FailedAttempts++;

        if (FailedAttempts >= 5)
            IsBlocked = true;

        SetUpdated();
    }

    public void Unblock()
    {
        FailedAttempts = 0;
        IsBlocked = false;
        SetUpdated();
    }
}