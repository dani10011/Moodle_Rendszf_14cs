using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moodle.Core;
using Newtonsoft.Json;
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


            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory
            string jsonFilePath = Path.Combine(projectRoot, "Moodle.Core/Jsons/LoginInfo.json");

            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            List<Login> users = JsonConvert.DeserializeObject<List<Login>>(jsonData);
            
            foreach (Login login in users)
            {
                if(login.username == loginData.Username && login.password == loginData.Password)
                {
                    return Ok("Login successful!");
                }
            }

            return Unauthorized("Invalid credentials");
        }
        
    }
}
