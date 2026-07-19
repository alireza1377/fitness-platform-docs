using Fitness.Application.Interfaces;
using Fitness.Application.Services;
using Fitness.Infrastructure.Database.Context;
using Fitness.Infrastructure.Repositories;
using Fitness.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Fitness.Infrastructure.Configuration;

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
services.Configure<JwtOptions>(
    configuration.GetSection(JwtOptions.SectionName));
       services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IJwtService, JwtService>();

services.AddScoped<IAuthIdentityRepository, AuthIdentityRepository>();
services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

services.AddScoped<IOtpService, RedisOtpService>();
services.AddScoped<ISmsService, SmsService>();

services.AddScoped<IAuthService, AuthService>();

services.AddHttpContextAccessor();
services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}