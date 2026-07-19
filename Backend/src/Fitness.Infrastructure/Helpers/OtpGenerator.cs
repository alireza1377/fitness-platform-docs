namespace Fitness.Infrastructure.Helpers;

public static class OtpGenerator
{
    public static string Generate()
    {
        return Random.Shared.Next(100000, 999999).ToString();
    }
}