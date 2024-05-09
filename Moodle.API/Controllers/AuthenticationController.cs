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
        private readonly  IConfiguration configuration;

        public AuthenticationController(MoodleDbContext context, IConfiguration _configuration)
        {
            _context = context;
            configuration = _configuration;
        }

        [HttpPost("login")] // Change to match the Javascript request
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
                    Token token = new Token(configuration);
                    var accessToken = token.GenerateAccessToken(aktualis_felhasznalo);  // Call a new method to generate access token
                    var refreshToken = token.GenerateRefreshToken();

                    return Ok(new { message = "Login successful!", userId = aktualis_felhasznalo.Id, role = aktualis_felhasznalo.Role, token = accessToken, refreshtoken = refreshToken });
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
                
            }

        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            }

            try
            {
                Token token = new Token(configuration);
                var aktualis_felhasznalo = _context.Users.SingleOrDefault();
                var newAccessToken = token.GenerateAccessToken(aktualis_felhasznalo);
                if (newAccessToken == null)
                {
                    return Unauthorized("Invalid refresh token");
                }

                return Ok(new { accessToken = newAccessToken });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing token: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error refreshing token");
            }
        }

    }
}
