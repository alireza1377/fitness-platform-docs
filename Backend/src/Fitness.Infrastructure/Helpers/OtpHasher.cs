using System.Security.Cryptography;
using System.Text;

namespace Fitness.Infrastructure.Helpers;

public static class OtpHasher
{
    public static string Hash(string otp)
    {
        using var sha = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(otp);

        var hash = sha.ComputeHash(bytes);

        return Convert.ToHexString(hash);
    }
}