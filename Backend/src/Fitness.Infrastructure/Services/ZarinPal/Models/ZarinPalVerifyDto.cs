namespace Fitness.Infrastructure.Services.ZarinPal.Models;

public class ZarinPalVerifyDto
{
    public string MerchantId { get; set; } = string.Empty;

    public string Authority { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}