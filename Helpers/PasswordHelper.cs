using System.Security.Cryptography;
using System.Text;


namespace LogansDBTest.Helpers {

    public static class PasswordHelper
    {
         public static string HashPassword(string password) {
             using(var sha256 = SHA256.Create()) {
              // Convert password to bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Compute hash
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert hash to Base64 string
                return Convert.ToBase64String(hash);
            }
         }
    
    }   
}