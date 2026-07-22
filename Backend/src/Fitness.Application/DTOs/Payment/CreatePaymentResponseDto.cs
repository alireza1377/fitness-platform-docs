namespace Fitness.Application.DTOs.Payment;

public class CreatePaymentResponseDto
{
    public Guid PaymentId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;
}