using Microsoft.AspNetCore.Mvc;
using Moodle.Data;
using Moodle.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly MoodleDbContext _context;
        private readonly IConfiguration configuration;

        public AuthenticationController(MoodleDbContext context, IConfiguration _configuration)
        {
            _context = context;
            configuration = _configuration;
        }
        
        

        [HttpPost("login")] //bejelentkezési függvény
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            if (loginData == null)
            {
                return BadRequest("Invalid request body");
            }

   
            var aktualis_felhasznalo = _context.Users.SingleOrDefault(p => p.UserName == loginData.Username);
            if (aktualis_felhasznalo == null)
            {
                return Unauthorized("Invalid credentials");
            }
            else
            {
                

                if (Hashing.VerifyPassword(aktualis_felhasznalo.Password, loginData.Password, aktualis_felhasznalo.UserName))
                {
                    //Sikeres bejelentkezés -> token generálás
                    var accessToken = GenerateAccessToken(aktualis_felhasznalo);  // Call a new method to generate access token

                    return Ok(new { message = "Login successful!", userId = aktualis_felhasznalo.Id, role = aktualis_felhasznalo.Role, name = aktualis_felhasznalo.Name, token = accessToken });
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
                
            }

        }


        private string GenerateAccessToken(User user)
        {
            // konfiguráljuk a tokent
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  // User ID claim
                new Claim(ClaimTypes.Role, user.Role),  // User role claim
                // továbbiak beállítása itt
                };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"])); // titkos kulcs visszaszerzése
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(30), // beaállítható meddig legyen aktív
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
