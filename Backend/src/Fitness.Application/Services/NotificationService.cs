using Fitness.Application.DTOs.Notification;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Domain.Enums;

namespace Fitness.Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;

    public NotificationService(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<NotificationDto>> GetNotificationsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var notifications =
            await _repository.GetByUserAsync(
                userId,
                cancellationToken);

        return notifications.Select(x => new NotificationDto
        {
            Id = x.Id,
            Title = x.Title,
            Message = x.Message,
            Type = x.Type,
            IsRead = x.IsRead,
            CreatedAt = x.CreatedAt
        }).ToList();
    }

    public Task<int> GetUnreadCountAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetUnreadCountAsync(
            userId,
            cancellationToken);
    }

    public async Task MarkAsReadAsync(
        Guid notificationId,
        CancellationToken cancellationToken = default)
    {
        var notification =
            await _repository.GetByIdAsync(
                notificationId,
                cancellationToken);

        if (notification is null)
            return;

        notification.MarkAsRead();

        await _repository.UpdateAsync(
            notification,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task MarkAllAsReadAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var notifications =
            await _repository.GetByUserAsync(
                userId,
                cancellationToken);

        foreach (var item in notifications)
            item.MarkAsRead();

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task DeleteAsync(
        Guid notificationId,
        CancellationToken cancellationToken = default)
    {
        var notification =
            await _repository.GetByIdAsync(
                notificationId,
                cancellationToken);

        if (notification is null)
            return;

        await _repository.DeleteAsync(
            notification,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }

    public async Task CreateAsync(
        Guid userId,
        string title,
        string message,
        NotificationType type,
        CancellationToken cancellationToken = default)
    {
        var notification =
            new Notification(
                userId,
                title,
                message,
                type);

        await _repository.AddAsync(
            notification,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);
    }
}