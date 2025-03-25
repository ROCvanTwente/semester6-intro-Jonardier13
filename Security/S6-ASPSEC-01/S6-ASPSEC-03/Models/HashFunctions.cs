using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;


namespace S6_ASPSEC_03.Models
{
    public static class HashFunctions
    {
        public static string HashText(string text, string algorithm)
        {
            HashAlgorithm hasher = null;

            if (algorithm == "MD5")
            {
                hasher = MD5.Create();
            }
            else if (algorithm == "SHA-1")
            {
                hasher = SHA1.Create();
            }
            else if (algorithm == "SHA256")
            {
                hasher = SHA256.Create();
            }
            else if (algorithm == "SHA512")
            {
                hasher = SHA512.Create();
            }

            if (hasher != null)
            {
                byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            else if (algorithm == "BCrypt")
            {
                return BCrypt.Net.BCrypt.HashPassword(text);
            }

            return "Ongeldig algoritme!";
        }
    }
}