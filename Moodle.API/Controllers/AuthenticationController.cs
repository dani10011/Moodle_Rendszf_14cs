using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moodle.Core;
using Moodle.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Moodle.Data.Entities;


namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly MoodleDbContext _context;

        public AuthenticationController(MoodleDbContext context)
        {
            _context = context;
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
                

                if (loginData.Password == aktualis_felhasznalo.Password)
                {

                    return Ok(new { message = "Login successful!", userId = aktualis_felhasznalo.Id });
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
                
            }

        }
        
    }
}
