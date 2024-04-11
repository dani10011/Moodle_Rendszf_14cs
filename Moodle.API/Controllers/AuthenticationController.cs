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
            string loginInfoPath = Path.Combine(projectRoot, "Moodle.Core/Jsons/LoginInfo.json");

            string loginInfoData = System.IO.File.ReadAllText(loginInfoPath);
            List<Login> loginInfo = JsonConvert.DeserializeObject<List<Login>>(loginInfoData);
            
            foreach (Login login in loginInfo)
            {
                if(login.username == loginData.Username && login.password == loginData.Password)
                {
                    //  Először kiveszi az összes felhasználó közül az aktuális felhasználót,

                    string usersPath = Path.Combine(projectRoot, "Moodle.Core/Jsons/Users.json");

                    var usersJson = System.IO.File.ReadAllText(usersPath);
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);

                    List<User> filteredUser = users.Where(c => c.Neptun_code.Contains(loginData.Username)).ToList();

                    User currentUser = filteredUser[0];

                    //  Utána belerakja a CurrentUser.json file-ba

                    string currentUserPath = Path.Combine(projectRoot, "Moodle.Core/Jsons/CurrentUser.json");

                    string currentUserJson = System.IO.File.ReadAllText(currentUserPath);

                    dynamic currentUserObj = Newtonsoft.Json.JsonConvert.DeserializeObject(currentUserJson);

                    
                    currentUserObj["name"] = currentUser.Name;
                    currentUserObj["neptun_code"] = currentUser.Neptun_code;
                    currentUserObj["degree"] = currentUser.Degree;
                    
                    
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(currentUserObj, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(currentUserPath, output);

                    //
                    return Ok("Login successful!");
                }
            }

            return Unauthorized("Invalid credentials");
        }
        
    }
}
