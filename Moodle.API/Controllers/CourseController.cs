using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Moodle.Core;


namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetCoursesByID(string neptun)
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            var filePath = System.IO.Path.Combine(basePath, "..", "\\Moodle.Core\\Jsons\\course.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var json = System.IO.File.ReadAllText(filePath);

            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);

            List<Course> filteredCourses = courses.Where(c => c.enrolled_students.Contains(neptun)).ToList();

            string newJson = JsonConvert.SerializeObject(filteredCourses, Formatting.Indented);

            return this.Content(newJson, "application/json");
        }

        [HttpGet]
        public IActionResult CheckAcceptedDegrees(string degree)
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            var filePath = System.IO.Path.Combine(basePath, "..", "\\Moodle.Core\\Jsons\\course.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var json = System.IO.File.ReadAllText(filePath);

            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);

            List<Course> filteredCourses = courses.Where(c => c.approved_degrees.Contains(degree)).ToList();

            string newJson = JsonConvert.SerializeObject(filteredCourses, Formatting.Indented);

            return this.Content(newJson, "application/json");
        }
    }
}
