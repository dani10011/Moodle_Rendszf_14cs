using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public CourseController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //saját kurzusok current userből
        //szűrés
        //degree megfelelő-e
        /*
        [HttpGet]
        public IActionResult GetCoursesByID(string neptun) 
        {
            var filePath = Path.GetRelativePath(_environment.ContentRootPath, "\\Moodle.Core\\Jsons\\course.json");
            //string jsonString = file.ReadAllText(@"..\\Moodle.Core\\Jsons\\course.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var json = System.IO.File.ReadAllText(filePath);

            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);

            string jsonString = json.ToString();
            return Ok("");
        }
        */
    }
}
