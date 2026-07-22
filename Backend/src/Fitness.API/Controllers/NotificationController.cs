using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Authorize]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly ICurrentUserService _currentUser;

    public NotificationController(
        INotificationService notificationService,
        ICurrentUserService currentUser)
    {
        _notificationService = notificationService;
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
            return Unauthorized();

        var notifications =
            await _notificationService.GetNotificationsAsync(
                _currentUser.UserId.Value,
                cancellationToken);

        return Ok(notifications);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount(
        CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
            return Unauthorized();

        var count =
            await _notificationService.GetUnreadCountAsync(
                _currentUser.UserId.Value,
                cancellationToken);

        return Ok(count);
    }

    [HttpPut("{id:guid}/read")]
    public async Task<IActionResult> Read(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _notificationService.MarkAsReadAsync(
            id,
            cancellationToken);

        return NoContent();
    }

    [HttpPut("read-all")]
    public async Task<IActionResult> ReadAll(
        CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
            return Unauthorized();

        await _notificationService.MarkAllAsReadAsync(
            _currentUser.UserId.Value,
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _notificationService.DeleteAsync(
            id,
            cancellationToken);

        return NoContent();
    }
}