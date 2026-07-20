using System.Text.Json;
using Fitness.Application.Exceptions;
using Fitness.Application.Interfaces;
using Fitness.Infrastructure.Helpers;
using Fitness.Infrastructure.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Fitness.Infrastructure.Services;

public class RedisOtpService : IOtpService
{
    private readonly IDistributedCache _cache;
    private readonly ISmsService _smsService;

    private static readonly TimeSpan OtpLifetime = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan OtpCooldown = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan OtpHourlyWindow = TimeSpan.FromHours(1);

    private const int MaxFailedAttempts = 5;
    private const int MaxOtpPerHour = 5;

    public RedisOtpService(
        IDistributedCache cache,
        ISmsService smsService)
    {
        _cache = cache;
        _smsService = smsService;
    }

    public async Task SendOtpAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        var key = $"otp:{phoneNumber}";
        var cooldownKey = $"otp:cooldown:{phoneNumber}";
        var rateLimitKey = $"otp:rate:{phoneNumber}";

        // محدودیت ۵ درخواست در ساعت
        var countText = await _cache.GetStringAsync(
            rateLimitKey,
            cancellationToken);

        var count = 0;

        if (!string.IsNullOrWhiteSpace(countText))
            count = int.Parse(countText);

        if (count >= MaxOtpPerHour)
            throw new Exception("حداکثر ۵ درخواست OTP در یک ساعت مجاز است.");

        // جلوگیری از ارسال پشت سر هم
        var cooldownExists = await _cache.GetStringAsync(
            cooldownKey,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(cooldownExists))
            throw new OtpAlreadySentException();

        // تولید OTP
        var otp = OtpGenerator.Generate();

        var model = new OtpCacheModel
        {
            CodeHash = OtpHasher.Hash(otp),
            CreatedAt = DateTime.UtcNow,
            FailedAttempts = 0
        };

        var json = JsonSerializer.Serialize(model);

        // ذخیره OTP
        await _cache.SetStringAsync(
            key,
            json,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = OtpLifetime
            },
            cancellationToken);

        // ایجاد Cooldown
        await _cache.SetStringAsync(
            cooldownKey,
            "1",
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = OtpCooldown
            },
            cancellationToken);

        // افزایش شمارنده درخواست‌ها
        count++;

        await _cache.SetStringAsync(
            rateLimitKey,
            count.ToString(),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = OtpHourlyWindow
            },
            cancellationToken);

        // ارسال پیامک
        await _smsService.SendOtpAsync(
            phoneNumber,
            otp,
            cancellationToken);
    }

    public async Task VerifyOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken = default)
    {
        var key = $"otp:{phoneNumber}";

        var json = await _cache.GetStringAsync(
            key,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(json))
            throw new OtpExpiredException();

        var model = JsonSerializer.Deserialize<OtpCacheModel>(json);

        if (model is null)
            throw new OtpExpiredException();

        // بررسی زمان اعتبار OTP
        var remaining =
            OtpLifetime - (DateTime.UtcNow - model.CreatedAt);

        if (remaining <= TimeSpan.Zero)
        {
            await _cache.RemoveAsync(
                key,
                cancellationToken);

            throw new OtpExpiredException();
        }

        var hash = OtpHasher.Hash(code);

        // کد اشتباه
        if (hash != model.CodeHash)
        {
            model.FailedAttempts++;

            if (model.FailedAttempts >= MaxFailedAttempts)
            {
                await _cache.RemoveAsync(
                    key,
                    cancellationToken);

                throw new TooManyOtpAttemptsException();
            }

            var updatedJson = JsonSerializer.Serialize(model);

            await _cache.SetStringAsync(
                key,
                updatedJson,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = remaining
                },
                cancellationToken);

            throw new InvalidOtpException();
        }

        // OTP صحیح
        await _cache.RemoveAsync(
            key,
            cancellationToken);
    }
}