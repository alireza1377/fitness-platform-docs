namespace Fitness.Infrastructure.Models;

public class OtpCacheModel
{
    public string CodeHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public int FailedAttempts { get; set; }
}