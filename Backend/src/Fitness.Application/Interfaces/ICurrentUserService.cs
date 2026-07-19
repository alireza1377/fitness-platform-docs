namespace Fitness.Application.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }

    bool IsGuest { get; }

    bool IsAuthenticated { get; }
}