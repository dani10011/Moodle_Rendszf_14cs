using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
    public class Token
    {
        private readonly IConfiguration configuration;

        public Token(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public static string TokenGenerator()
        {
            var randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] secretKey = new byte[32]; // Replace 32 with desired key length (in bytes)
            randomNumberGenerator.GetBytes(secretKey);
            string secretKeyString = Convert.ToBase64String(secretKey);

            Console.WriteLine("Your secret key: " + secretKeyString);

            return secretKeyString;
            
        }

        public string GenerateAccessToken(User user)
        {
            // Configure claims for the access token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  // User ID claim
                new Claim(ClaimTypes.Role, user.Role),  // User role claim
                // You can add other relevant claims here (e.g., username, email)
                };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"])); // Retrieve secret key from configuration
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(30), // Set short expiration time (e.g., 15 minutes)
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public string GenerateRefreshToken()
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenGenerator())); // Retrieve secret key from configuration
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(2), // Set short expiration time (e.g., 15 minutes)
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }

    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
