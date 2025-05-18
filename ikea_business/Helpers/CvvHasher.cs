using System.Security.Cryptography;
using System.Text;

namespace ikea_business.Helpers;

public static class CvvHasher
{
    public static byte[] Hash(string cvv)
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(Encoding.UTF8.GetBytes(cvv));
    }
}