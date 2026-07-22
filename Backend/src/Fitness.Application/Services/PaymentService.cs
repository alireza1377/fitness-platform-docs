using Fitness.Application.DTOs.Payment;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IZarinPalGateway _zarinPalGateway;

    public PaymentService(
        IPaymentRepository paymentRepository,
        IZarinPalGateway zarinPalGateway)
    {
        _paymentRepository = paymentRepository;
        _zarinPalGateway = zarinPalGateway;
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

        var gatewayResult =
            await _zarinPalGateway.CreatePaymentRequestAsync(
                payment.Id,
                payment.Amount,
                payment.Description ?? "Fitness Subscription",
                cancellationToken);

        if (!gatewayResult.Success)
        {
            throw new Exception(
                gatewayResult.ErrorMessage ??
                "Unable to create payment request.");
        }

        payment.SetAuthority(gatewayResult.Authority!);

        await _paymentRepository.UpdateAsync(
            payment,
            cancellationToken);

        await _paymentRepository.SaveChangesAsync(
            cancellationToken);

        return new CreatePaymentResponseDto
        {
            PaymentId = payment.Id,
            Amount = payment.Amount,
            Status = payment.Status.ToString(),
            Authority = gatewayResult.Authority,
            PaymentUrl = gatewayResult.PaymentUrl
        };
    }

    public async Task VerifyPaymentAsync(
        Guid paymentId,
        string authority,
        CancellationToken cancellationToken = default)
    {
        var payment =
            await _paymentRepository.GetByIdAsync(
                paymentId,
                cancellationToken);

        if (payment is null)
            throw new Exception("Payment not found.");

        var verifyResult =
            await _zarinPalGateway.VerifyPaymentAsync(
                authority,
                payment.Amount,
                cancellationToken);

        if (!verifyResult.Success)
        {
            payment.MarkFailed();

            await _paymentRepository.UpdateAsync(
                payment,
                cancellationToken);

            await _paymentRepository.SaveChangesAsync(
                cancellationToken);

            throw new Exception(
                verifyResult.ErrorMessage ??
                "Payment verification failed.");
        }

        payment.MarkSucceeded(
            authority,
            verifyResult.RefId!,
            verifyResult.TrackingCode!);

        await _paymentRepository.UpdateAsync(
            payment,
            cancellationToken);

        await _paymentRepository.SaveChangesAsync(
            cancellationToken);
    }
}