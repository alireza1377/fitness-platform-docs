using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class Notification : AuditableEntity
{
    public Guid UserId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Message { get; private set; } = string.Empty;

    public NotificationType Type { get; private set; }

    public bool IsRead { get; private set; }

    public DateTime? ReadAt { get; private set; }

    public User User { get; private set; } = null!;

    private Notification()
    {
    }

    public Notification(
        Guid userId,
        string title,
        string message,
        NotificationType type)
    {
        UserId = userId;
        Title = title;
        Message = message;
        Type = type;

        IsRead = false;
    }

    public void MarkAsRead()
    {
        if (IsRead)
            return;

        IsRead = true;
        ReadAt = DateTime.UtcNow;

        SetUpdated();
    }

    public void MarkAsUnread()
    {
        IsRead = false;
        ReadAt = null;

        SetUpdated();
    }

    public void Update(
        string title,
        string message,
        NotificationType type)
    {
        Title = title;
        Message = message;
        Type = type;

        SetUpdated();
    }
}