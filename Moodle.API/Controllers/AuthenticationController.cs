using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class Authentication
        {
            public string Username { get; set; }
            public string Password { get; set; }

        }

        [HttpPost("login")]
        public IActionResult PostData([FromBody] Authentication data)
        {

            // Now you can access data.Field1 and data.Field2

            // Do something with the data...

            //teszt
            string jsonString = "sikeres json atadas ujra!!!!!";
            string json = JsonSerializer.Serialize(jsonString);
            return this.Content(json, "application/json");
            //

            //return Ok(); // Return a 200 OK response
        }
    }
}
