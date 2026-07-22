using Fitness.Domain.Enums;

namespace Fitness.Application.DTOs.Payment;

public class CreatePaymentRequestDto
{
    public decimal Amount { get; set; }

    public PaymentGateway Gateway { get; set; }

    public string Description { get; set; } = string.Empty;
}

