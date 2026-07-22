using Fitness.Application.DTOs.Payment;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<CreatePaymentResponseDto> CreatePaymentAsync(
        Guid userId,
        CreatePaymentRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var payment = new Payment(
            userId,
            request.Amount,
            request.Gateway,
            request.Description);

        await _paymentRepository.AddAsync(
            payment,
            cancellationToken);

        await _paymentRepository.SaveChangesAsync(
            cancellationToken);

        return new CreatePaymentResponseDto
        {
            PaymentId = payment.Id,
            Amount = payment.Amount,
            Status = payment.Status.ToString()
        };
    }
}