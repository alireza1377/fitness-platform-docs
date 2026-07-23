namespace Fitness.Application.DTOs.Payment;

public class PaymentHistoryItemDto
{
    public Guid PaymentId { get; set; }

    public decimal Amount { get; set; }

    public string Gateway { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public string Description { get; set; } = string.Empty;

    public string? TrackingCode { get; set; }
}