using Fitness.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Fitness.Infrastructure.Services;

public class SmsService : ISmsService
{
    private readonly ILogger<SmsService> _logger;

    public SmsService(ILogger<SmsService> logger)
    {
        _logger = logger;
    }

    public Task SendOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "OTP for {PhoneNumber}: {Code}",
            phoneNumber,
            code);

        return Task.CompletedTask;
    }
}
