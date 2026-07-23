using Fitness.Application.DTOs.Payment;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
    private readonly IZarinPalGateway _zarinPalGateway;
    private readonly ISubscriptionActivationService _subscriptionActivationService;

    public PaymentService(
        IPaymentRepository paymentRepository,
        ISubscriptionPlanRepository subscriptionPlanRepository,
        IZarinPalGateway zarinPalGateway,
        ISubscriptionActivationService subscriptionActivationService)
    {
        _paymentRepository = paymentRepository;
        _subscriptionPlanRepository = subscriptionPlanRepository;
        _zarinPalGateway = zarinPalGateway;
        _subscriptionActivationService = subscriptionActivationService;
    }

    public async Task<CreatePaymentResponseDto> CreatePaymentAsync(
        Guid userId,
        CreatePaymentRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var plan =
            await _subscriptionPlanRepository.GetByIdAsync(
                request.PlanId,
                cancellationToken);

        if (plan is null)
            throw new Exception("Subscription plan not found.");

        if (!plan.IsActive)
            throw new Exception("Subscription plan is inactive.");

        var payment = new Payment(
            userId,
            plan.Id,
            plan.Price,
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

        var plan =
            await _subscriptionPlanRepository.GetByIdAsync(
                payment.SubscriptionPlanId,
                cancellationToken);

        if (plan is null)
            throw new Exception("Subscription plan not found.");

        await _subscriptionActivationService.CreateSubscriptionAsync(
            payment.UserId,
            plan.Type,
            payment.RefId ?? string.Empty,
            cancellationToken);
    }

    public async Task<List<PaymentHistoryItemDto>> GetHistoryAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var payments =
            await _paymentRepository.GetUserPaymentsAsync(
                userId,
                cancellationToken);

        return payments
            .Select(x => new PaymentHistoryItemDto
            {
                PaymentId = x.Id,
                Amount = x.Amount,
                Gateway = x.Gateway.ToString(),
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt,
                PaidAt = x.PaidAt,
                Description = x.Description,
                TrackingCode = x.TrackingCode
            })
            .ToList();
    }
}