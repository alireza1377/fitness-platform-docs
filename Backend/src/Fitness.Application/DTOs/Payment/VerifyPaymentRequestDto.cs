namespace Fitness.Application.DTOs.Payment;

public class VerifyPaymentRequestDto
{
    public Guid PaymentId { get; set; }

    public string Authority { get; set; } = string.Empty;
}