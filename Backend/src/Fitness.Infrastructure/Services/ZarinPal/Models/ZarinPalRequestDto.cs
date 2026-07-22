namespace Fitness.Infrastructure.Services.ZarinPal.Models;

public class ZarinPalRequestDto
{
    public string MerchantId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public string CallbackUrl { get; set; } = string.Empty;
}