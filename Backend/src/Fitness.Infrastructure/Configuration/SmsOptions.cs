namespace Fitness.Infrastructure.Configuration;

public class SmsOptions
{
    public const string SectionName = "Sms";

    public string Username { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string From { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}