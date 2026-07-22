namespace Fitness.Application.DTOs.Payment;

public class PaymentRequestResultDto
{
    public bool Success { get; set; }

    public string Authority { get; set; } = string.Empty;

    public string PaymentUrl { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }
}