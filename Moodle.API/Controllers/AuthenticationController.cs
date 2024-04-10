using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        public class LoginData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")] // Change to match the Javascript request
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            if (loginData == null)
            {
                return BadRequest("Invalid request body");
            }

            // Simulate user validation (replace with your actual logic)
            if (loginData.Username == "user" && loginData.Password == "password")
            {
                return Ok("Login successful!");
            }

            return Unauthorized("Invalid credentials");
        }
        
    }
}
