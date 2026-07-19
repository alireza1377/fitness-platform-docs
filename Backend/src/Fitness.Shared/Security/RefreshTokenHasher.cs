using System.Security.Cryptography;
using System.Text;

namespace Fitness.Shared.Security;

public static class RefreshTokenHasher
{
    public static string Hash(string value)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(value);

        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}