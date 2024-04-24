using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Core
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Hashing
    {
        public static string HashPassword(string password, string username)
        {
            byte[] salt = Encoding.UTF8.GetBytes(username);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256); 
            byte[] hash = pbkdf2.GetBytes(32); 

            // Combine salt and hash into a single string for storage
            var hashBytes = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword, string username)
        {
            
            byte[] salt = Encoding.UTF8.GetBytes(username);

            var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, 10000, HashAlgorithmName.SHA256);
            byte[] newHash = pbkdf2.GetBytes(32); 

            return newHash.SequenceEqual(Convert.FromBase64String(hashedPassword)); 
        }
    }
}
