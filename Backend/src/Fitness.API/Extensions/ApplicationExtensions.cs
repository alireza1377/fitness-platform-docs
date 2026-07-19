using Fitness.Application.Interfaces;
using Fitness.Infrastructure.Services;

namespace Fitness.API.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ISmsService, SmsService>();

        services.AddScoped<IOtpService, RedisOtpService>();

        return services;
    }
}