using Fitness.Application.DTOs.Dashboard;

namespace Fitness.Application.Dashboard.Builders;

public class NotificationBuilder
{
    public Task<List<NotificationCardDto>> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var notifications = new List<NotificationCardDto>
        {
            new NotificationCardDto
            {
                Title = "به Fitness خوش آمدید 🎉",
                Message = "تمرین امروز را از دست نده.",
                Type = NotificationType.Info,
                CreatedAt = DateTime.UtcNow
            }
        };

        return Task.FromResult(notifications);
    }
}