using System.Net.Http.Json;
using Fitness.Application.DTOs.Payment;
using Fitness.Application.Interfaces;
using Fitness.Infrastructure.Configuration;
using Fitness.Infrastructure.Services.ZarinPal.Models;
using Microsoft.Extensions.Options;

namespace Fitness.Infrastructure.Services;

public class ZarinPalGateway : IZarinPalGateway
{
    private readonly HttpClient _httpClient;
    private readonly ZarinPalOptions _options;

    public ZarinPalGateway(
        HttpClient httpClient,
        IOptions<ZarinPalOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<PaymentRequestResultDto> CreatePaymentRequestAsync(
        Guid paymentId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default)
    {
        var request = new ZarinPalRequestDto
        {
            MerchantId = _options.MerchantId,
            Amount = amount,
            Description = description,
            CallbackUrl = $"{_options.CallbackUrl}?paymentId={paymentId}"
        };

        var response =
            await _httpClient.PostAsJsonAsync(
                _options.RequestUrl,
                request,
                cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new PaymentRequestResultDto
            {
                Success = false,
                ErrorMessage = "Connection to ZarinPal failed."
            };
        }

        var result =
            await response.Content.ReadFromJsonAsync<ZarinPalRequestResponseDto>(
                cancellationToken: cancellationToken);

        if (result?.Data?.Code != 100)
        {
            return new PaymentRequestResultDto
            {
                Success = false,
                ErrorMessage = result?.Errors?.Message
            };
        }

        return new PaymentRequestResultDto
        {
            Success = true,
            Authority = result.Data.Authority,
            PaymentUrl =
                $"https://sandbox.zarinpal.com/pg/StartPay/{result.Data.Authority}"
        };
    }

    public async Task<PaymentVerifyResultDto> VerifyPaymentAsync(
        string authority,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        var request = new ZarinPalVerifyDto
        {
            MerchantId = _options.MerchantId,
            Authority = authority,
            Amount = amount
        };

        var response =
            await _httpClient.PostAsJsonAsync(
                _options.VerifyUrl,
                request,
                cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new PaymentVerifyResultDto
            {
                Success = false,
                ErrorMessage = "Verify request failed."
            };
        }

        var result =
            await response.Content.ReadFromJsonAsync<ZarinPalVerifyResponseDto>(
                cancellationToken: cancellationToken);

        if (result?.Data?.Code != 100 &&
            result?.Data?.Code != 101)
        {
            return new PaymentVerifyResultDto
            {
                Success = false,
                ErrorMessage = result?.Errors?.Message
            };
        }

        return new PaymentVerifyResultDto
        {
            Success = true,
            RefId = result.Data.RefId.ToString(),
            TrackingCode = result.Data.RefId.ToString()
        };
    }
}