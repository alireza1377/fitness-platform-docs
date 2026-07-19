namespace Fitness.Application.Interfaces;

public interface ISmsService
{
    Task SendOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken = default);
}