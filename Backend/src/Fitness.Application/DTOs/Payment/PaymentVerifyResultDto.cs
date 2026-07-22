namespace Fitness.Application.DTOs.Payment;

public class PaymentVerifyResultDto
{
    public bool Success { get; set; }

    public string RefId { get; set; } = string.Empty;

    public string TrackingCode { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }
}