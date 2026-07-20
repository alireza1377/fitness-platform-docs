using Fitness.Application.Interfaces;
using Fitness.Application.Services;
using Fitness.Infrastructure.Configuration;
using Fitness.Infrastructure.Database.Context;
using Fitness.Infrastructure.Repositories;
using Fitness.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Fitness.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FitnessDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        // Configuration
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.Configure<SmsOptions>(
            configuration.GetSection(SmsOptions.SectionName));

        // HttpClient
        services.AddHttpClient<ISmsService, SmsService>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthIdentityRepository, AuthIdentityRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        // Services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IOtpService, RedisOtpService>();
        services.AddScoped<ISmsService, SmsService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProfileService, ProfileService>();

        // Current User
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}