using Fitness.Application.DTOs.Payment;

namespace Fitness.Application.Interfaces;

public interface IPaymentService
{
    Task<CreatePaymentResponseDto> CreatePaymentAsync(
        Guid userId,
        CreatePaymentRequestDto request,
        CancellationToken cancellationToken = default);

        Task VerifyPaymentAsync(
    Guid paymentId,
    string authority,
    CancellationToken cancellationToken = default);

    Task<List<PaymentHistoryItemDto>> GetHistoryAsync(
    Guid userId,
    CancellationToken cancellationToken = default);
}