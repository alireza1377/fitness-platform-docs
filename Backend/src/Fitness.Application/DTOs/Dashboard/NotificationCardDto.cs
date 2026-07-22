namespace Fitness.Application.DTOs.Dashboard;

public class NotificationCardDto
{
    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public NotificationType Type { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum NotificationType
{
    Info,
    Success,
    Warning,
    Workout,
    Subscription
}