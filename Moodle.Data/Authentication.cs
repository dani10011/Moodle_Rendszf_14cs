using System.Security.Cryptography;
using System.Text;
using Moodle.Data.Entities;

namespace Moodle.Data
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Hashing
    {
        public Hashing() { }
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


        public static List<User> HashAllUserPasswords(List<User> users)
        {
            List<User> hashed = new List<User>();
            hashed = users;
            foreach (var user in hashed) 
            {
                user.Password = HashPassword(user.Password, user.UserName);
            }
            return hashed;
        }


        public static bool VerifyPassword(string hashedPassword, string providedPassword, string username)
        {
            
            byte[] salt = Encoding.UTF8.GetBytes(username);

            var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, 10000, HashAlgorithmName.SHA256);
            byte[] newHash = pbkdf2.GetBytes(32);

            var hashBytes = new byte[salt.Length + newHash.Length];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(newHash, 0, hashBytes, salt.Length, newHash.Length);

            //return newHash.SequenceEqual(Convert.FromBase64String(hashedPassword)); 
            return Convert.ToBase64String(hashBytes) == hashedPassword;
        }
    }
}
