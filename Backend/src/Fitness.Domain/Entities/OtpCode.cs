using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class OtpCode : AuditableEntity
{
    public string PhoneNumber { get; private set; } = string.Empty;

    public string CodeHash { get; private set; } = string.Empty;

    public OtpPurpose Purpose { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public DateTime? UsedAt { get; private set; }

    private OtpCode()
    {
    }

    public OtpCode(
        string phoneNumber,
        string codeHash,
        OtpPurpose purpose,
        DateTime expiresAt)
    {
        PhoneNumber = phoneNumber;
        CodeHash = codeHash;
        Purpose = purpose;
        ExpiresAt = expiresAt;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpiresAt;
    }

    public bool IsUsed()
    {
        return UsedAt != null;
    }

    public void MarkAsUsed()
    {
        UsedAt = DateTime.UtcNow;
        SetUpdated();
    }
}