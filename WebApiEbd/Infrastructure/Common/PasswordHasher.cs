using System.Security.Cryptography;
using System.Text;

namespace WebApiEbd.Infrastructure.Common;


public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("X2"));
        }
        return builder.ToString();
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var hashOfInput = HashPassword(password);
        return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hashedPassword) == 0;
    }
}