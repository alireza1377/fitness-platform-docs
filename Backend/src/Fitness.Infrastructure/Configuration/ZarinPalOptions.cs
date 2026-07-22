namespace Fitness.Infrastructure.Configuration;

public class ZarinPalOptions
{
    public const string SectionName = "ZarinPal";

    public string MerchantId { get; set; } = string.Empty;

    public string RequestUrl { get; set; } = string.Empty;

    public string VerifyUrl { get; set; } = string.Empty;

    public string CallbackUrl { get; set; } = string.Empty;

    public bool Sandbox { get; set; }
}