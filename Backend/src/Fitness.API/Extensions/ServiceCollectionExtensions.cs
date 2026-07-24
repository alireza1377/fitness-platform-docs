using Fitness.Application.Dashboard.Builders;
using Fitness.Application.Interfaces;
using Fitness.Application.Services;
using Fitness.Infrastructure.Configuration;
using Fitness.Infrastructure.Database.Context;
using Fitness.Infrastructure.Repositories;
using Fitness.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Fitness.Infrastructure.Services.Storage;
using Fitness.Infrastructure.Services.Video;

namespace Fitness.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ==========================
        // Database
        // ==========================
        services.AddDbContext<FitnessDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        // ==========================
        // Configuration
        // ==========================
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.Configure<SmsOptions>(
            configuration.GetSection(SmsOptions.SectionName));

        services.Configure<ZarinPalOptions>(
            configuration.GetSection(ZarinPalOptions.SectionName));

        // ==========================
        // Http Clients
        // ==========================
        services.AddHttpClient<ISmsService, SmsService>();

        services.AddHttpClient();

        // ==========================
        // Repositories
        // ==========================
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthIdentityRepository, AuthIdentityRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFitnessProgramRepository, FitnessProgramRepository>();
        services.AddScoped<IProgramVideoRepository, ProgramVideoRepository>();

        services.AddScoped<IUserVideoProgressRepository, UserVideoProgressRepository>();
        services.AddScoped<IUserProgramProgressRepository, UserProgramProgressRepository>();
        services.AddScoped<IUserStatisticsRepository, UserStatisticsRepository>();

        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ISubscriptionActivationService, SubscriptionActivationService>();

        // ==========================
        // Application Services
        // ==========================
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IOtpService, RedisOtpService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IContentService, ContentService>();
        services.AddScoped<IVideoPlayerService, VideoPlayerService>();
        services.AddScoped<IProgressService, ProgressService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<INotificationService, NotificationService>();

        // ==========================
        // Dashboard Builders
        // ==========================
        services.AddScoped<UserCardBuilder>();
        services.AddScoped<BmiBuilder>();
        services.AddScoped<StatisticsBuilder>();
        services.AddScoped<CurrentProgramBuilder>();
        services.AddScoped<WorkoutBuilder>();
        services.AddScoped<SubscriptionBuilder>();
        services.AddScoped<NotificationBuilder>();
        services.AddScoped<QuickActionBuilder>();

        // ==========================
        // Current User
        // ==========================
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // ==========================
        // Payment Gateway
        // ==========================
        // بعد از ساخت Gateway این خط را فعال می‌کنیم:
        // services.AddScoped<IZarinPalGateway, ZarinPalGateway>();
        services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
        services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
        
        services.AddScoped<IVideoStorageRepository, VideoStorageRepository>();

services.AddScoped<IFileStorageService, LocalFileStorageService>();
services.AddScoped<IVideoUploadService, VideoUploadService>();
services.AddScoped<IVideoStorageService, VideoStorageService>();
services.AddScoped<IVideoMetadataService, VideoMetadataService>();
services.AddScoped<IThumbnailGenerator, ThumbnailGenerator>();

        return services;
    }
}