using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Moodle.Core.Authentication;


namespace Moodle.API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        
        [HttpPost("login")]
        public IActionResult PostData([FromBody] Moodle.Core.Authentication data)
        {
            // Now you can access data.Field1 and data.Field2
            
            // Do something with the data...

            return Ok(); // Return a 200 OK response
        }
    }
}
