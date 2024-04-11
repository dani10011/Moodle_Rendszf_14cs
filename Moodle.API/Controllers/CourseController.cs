using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Moodle.Core;


namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        [HttpGet("allcourses")]
        public async Task<IActionResult> GetAllCourses()
        {

            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory
            string jsonFilePath = Path.Combine(projectRoot, "Moodle.Core/Jsons/course.json");

            string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            var json = System.IO.File.ReadAllText(jsonFilePath);

            return this.Content(json, "application/json");
        }

        [HttpGet("courseid")]
        public async Task<IActionResult> GetCoursesByID()
        {
            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory

            //Aktualis felhasznalo neptunkodjanak lekerese
            string userData = Path.Combine(projectRoot, "Moodle.Core/Jsons/CurrentUser.json");
            string userJson = System.IO.File.ReadAllText(userData);
            dynamic currentUser = JsonConvert.DeserializeObject(userJson);
            string neptun = currentUser["neptun_code"];

            //Kurzusok kigyujtese
            string jsonFilePath = Path.Combine(projectRoot, "Moodle.Core/Jsons/course.json");          

            string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            var json = System.IO.File.ReadAllText(jsonFilePath);

            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);

            //szures neptunkod szerint
            List<Course> filteredCourses = courses.Where(c => c.enrolled_students.Contains(neptun)).ToList();

            string newJson = JsonConvert.SerializeObject(filteredCourses, Formatting.Indented);

            return this.Content(newJson, "application/json");
        }

        [HttpGet("accepted")]
        public async Task<IActionResult> CheckAcceptedDegrees()
        {
            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory

            //Aktualis felhasznalo degree-jenek lekerese
            string userData = Path.Combine(projectRoot, "Moodle.Core/Jsons/CurrentUser.json");
            string userJson = System.IO.File.ReadAllText(userData);
            dynamic currentUser = JsonConvert.DeserializeObject(userJson);
            string degree = currentUser["degree"];

            //Kurzusok kigyujtese
            string jsonFilePath = Path.Combine(projectRoot, "Moodle.Core/Jsons/course.json");

            string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            var json = System.IO.File.ReadAllText(jsonFilePath);

            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);

            //szures degree szerint
            List<Course> filteredCourses = courses.Where(c => c.enrolled_students.Contains(degree)).ToList();

            string newJson = JsonConvert.SerializeObject(filteredCourses, Formatting.Indented);

            return this.Content(newJson, "application/json");
        }
    }
}
