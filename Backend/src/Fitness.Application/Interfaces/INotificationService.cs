using Fitness.Application.DTOs.Dashboard;

namespace Fitness.Application.Interfaces;

public interface INotificationService
{
    Task<List<NotificationCardDto>> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}