using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moodle.Core;
using Moodle.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Newtonsoft.Json.Linq;


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

                    // Clear the file content
                    System.IO.File.WriteAllText(currentUserPath, "");

                    // Define the attribute name and value
                    string attributeName = "ID";
                    int attributeValue = aktualis_felhasznalo.Id;

                    // Create a new JObject with the single attribute
                    JObject newJsonObject = new JObject(new JProperty(attributeName, attributeValue));

                    // Save the new object to the file
                    string jsonString = newJsonObject.ToString();

                    

                    //string output = Newtonsoft.Json.JsonConvert.SerializeObject(currentUserObj, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(currentUserPath, jsonString);

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
