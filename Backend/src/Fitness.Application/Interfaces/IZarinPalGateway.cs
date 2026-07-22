using Fitness.Application.DTOs.Payment;

namespace Fitness.Application.Interfaces;

public interface IZarinPalGateway
{
    Task<PaymentRequestResultDto> CreatePaymentRequestAsync(
        Guid paymentId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default);

    Task<PaymentVerifyResultDto> VerifyPaymentAsync(
        string authority,
        decimal amount,
        CancellationToken cancellationToken = default);
}