using System.Net.Http;
using Fitness.Application.Interfaces;
using Fitness.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fitness.Infrastructure.Services;

public class SmsService : ISmsService
{
    private readonly HttpClient _httpClient;
    private readonly SmsOptions _options;
    private readonly ILogger<SmsService> _logger;

    public SmsService(
        HttpClient httpClient,
        IOptions<SmsOptions> options,
        ILogger<SmsService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task SendOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken = default)
    {
        var values = new Dictionary<string, string>
        {
            ["username"] = _options.Username,
            ["password"] = _options.ApiKey,
            ["from"] = _options.From,
            ["to"] = phoneNumber,
            ["code"] = code
        };

        var content = new FormUrlEncodedContent(values);

        _logger.LogInformation(
            "Sending OTP SMS to {Phone}",
            phoneNumber);

        var response = await _httpClient.PostAsync(
            _options.Url,
            content,
            cancellationToken);

        var result = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogInformation(
            "Payamak Response: {Response}",
            result);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                $"Payamak HTTP Error ({response.StatusCode}): {result}");
        }

        // اگر سرویس REST خطا برگرداند
        if (result.Contains("\"RetStatus\":35") ||
            result.Contains("\"RetStatus\":0") ||
            result.Contains("\"RetStatus\":-1"))
        {
            throw new Exception($"Payamak Error: {result}");
        }

        _logger.LogInformation(
            "OTP SMS sent successfully to {Phone}",
            phoneNumber);
    }
}