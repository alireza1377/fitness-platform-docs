using Fitness.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Fitness.Infrastructure.Helpers;
using Fitness.Infrastructure.Models;
namespace Fitness.Infrastructure.Services;

public class RedisOtpService : IOtpService
{
   private readonly IDistributedCache _cache;
private readonly ISmsService _smsService;

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
    var otp = OtpGenerator.Generate();

    var model = new OtpCacheModel
    {
        CodeHash = OtpHasher.Hash(otp),
        CreatedAt = DateTime.UtcNow,
        FailedAttempts = 0
    };

    var json = JsonSerializer.Serialize(model);

    await _cache.SetStringAsync(
        $"otp:{phoneNumber}",
        json,
        new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        },
        cancellationToken);

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
    var json = await _cache.GetStringAsync(
        $"otp:{phoneNumber}",
        cancellationToken);

    if (string.IsNullOrWhiteSpace(json))
        throw new Exception("OTP expired.");

    var model = JsonSerializer.Deserialize<OtpCacheModel>(json);

    if (model is null)
        throw new Exception("OTP not found.");

    var hash = OtpHasher.Hash(code);

    if (hash != model.CodeHash)
        throw new Exception("OTP is invalid.");

    await _cache.RemoveAsync(
        $"otp:{phoneNumber}",
        cancellationToken);
}
}