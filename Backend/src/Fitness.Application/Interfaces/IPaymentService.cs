using Fitness.Application.DTOs.Payment;

namespace Fitness.Application.Interfaces;

public interface IPaymentService
{
    Task<CreatePaymentResponseDto> CreatePaymentAsync(
        Guid userId,
        CreatePaymentRequestDto request,
        CancellationToken cancellationToken = default);
}