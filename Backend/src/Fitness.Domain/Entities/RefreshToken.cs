using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class RefreshToken : AuditableEntity
{
    public Guid UserId { get; private set; }

    public string TokenHash { get; private set; } = string.Empty;

    public string DeviceId { get; private set; } = string.Empty;

    public string? UserAgent { get; private set; }

    public string? IpAddress { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public DateTime? RevokedAt { get; private set; }

    // Navigation
    public User User { get; private set; } = null!;

    private RefreshToken()
    {
    }

    public RefreshToken(
        Guid userId,
        string tokenHash,
        string deviceId,
        DateTime expiresAt,
        string? userAgent = null,
        string? ipAddress = null)
    {
        UserId = userId;
        TokenHash = tokenHash;
        DeviceId = deviceId;
        ExpiresAt = expiresAt;
        UserAgent = userAgent;
        IpAddress = ipAddress;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= ExpiresAt;
    }

    public bool IsRevoked()
    {
        return RevokedAt != null;
    }

    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
        SetUpdated();
    }
}