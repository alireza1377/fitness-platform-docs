using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly FitnessDbContext _context;

    public NotificationRepository(
        FitnessDbContext context)
    {
        _context = context;
    }

    public async Task<List<Notification>> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUnreadCountAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .CountAsync(
                x => x.UserId == userId && !x.IsRead,
                cancellationToken);
    }

    public async Task<Notification?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        await _context.Notifications.AddAsync(
            notification,
            cancellationToken);
    }

    public Task UpdateAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        _context.Notifications.Update(notification);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        _context.Notifications.Remove(notification);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}