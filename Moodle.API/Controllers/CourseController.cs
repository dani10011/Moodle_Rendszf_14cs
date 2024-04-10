using Microsoft.AspNetCore.Mvc;

namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        Dictionary<string, string> courses = new Dictionary<string, string>();
        public CourseController() 
        {
            
        }
        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok("");
        }
    }
}
