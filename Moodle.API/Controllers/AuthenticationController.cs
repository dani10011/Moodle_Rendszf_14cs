using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moodle.Core;
using Moodle.Data;
using Newtonsoft.Json;
using System.Text.Json;


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

            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName;
            string loginInfoPath = Path.Combine(projectRoot, "Moodle.Core/Jsons/LoginInfo.json");
            

            var aktualis_felhasznalo = _context.Users.SingleOrDefault(p => p.UserName == loginData.Username);
            if (aktualis_felhasznalo == null)
            {
                return Unauthorized("Invalid credentials");
            }
            else
            {
                

                if (loginData.Password == aktualis_felhasznalo.Password)
                 {
                    

                    string currentUserPath = Path.Combine(projectRoot, "Moodle.Core/Jsons/CurrentUser.json");
                    string currentUserJson = System.IO.File.ReadAllText(currentUserPath);
                    dynamic currentUserObj = Newtonsoft.Json.JsonConvert.DeserializeObject(currentUserJson);


                    currentUserObj["ID"] = aktualis_felhasznalo.Id;

                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(currentUserObj, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(currentUserPath, output);

                    return Ok("Login successful!");
                 }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
                
            }

        }
        
    }
}
