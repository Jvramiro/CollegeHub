using System.Security.Cryptography;

namespace CollegeHub.Extensions {
    public static class PasswordExtensions {

        public static string HashPassword(this string password) {
            SHA256 hash = SHA256.Create();
            var passwordBytes = System.Text.Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }

    }
}
