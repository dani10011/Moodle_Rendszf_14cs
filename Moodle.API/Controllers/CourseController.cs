using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Moodle.Data;
using Moodle.Data.Entities;


namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly MoodleDbContext context;
        public CourseController(MoodleDbContext _context)
        {
            this.context = _context;
        }

        [HttpGet("allcourses")]
        public IActionResult GetAllCourses()
        {
            var courses = context.Courses.ToList(); // Retrieve all courses from the database

            var json = JsonConvert.SerializeObject(courses, Formatting.Indented); // Serialize courses to JSON

            return Content(json, "application/json"); // Return JSON content


            //string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory
            //string jsonFilePath = Path.Combine(projectRoot, "Moodle.Core/Jsons/course.json");

            //string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            //var json = System.IO.File.ReadAllText(jsonFilePath);

            //return this.Content(json, "application/json");
        }

        [HttpGet("courseid")]
        public IActionResult GetCoursesByID()
        {
            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory

            //Aktualis felhasznalo idjanak lekerese
            string userData = Path.Combine(projectRoot, "Moodle.Core/Jsons/CurrentUser.json");
            string userJson = System.IO.File.ReadAllText(userData);
            dynamic currentUser = JsonConvert.DeserializeObject(userJson);
            int id = currentUser["ID"];

            Console.WriteLine(id);

            var myCourses = context.MyCourses.ToList();

            //var courseIDs = myCourses.Where(c => c.User_Id == id).ToList();

            var courses = context.Courses.ToList();

            List<int> courseIDs = new List<int>();

            foreach ( var course in myCourses)
            {
                if (course.User_Id == id)
                {
                    courseIDs.Add(course.Course_Id);
                }
            }

            List<Course> uCourses = new List<Course>();

            foreach (int i in courseIDs)
            {
                var course = courses.First(x => x.Id == i);
                uCourses.Add(course);
            }
            //var uCourses = courses.Where(c => c.Id.Equals(courseIDs)).ToList();

            var json = JsonConvert.SerializeObject(uCourses, Formatting.Indented);

            return Content(json, "application/json");

            

            ////Kurzusok kigyujtese
            //string jsonFilePath = Path.Combine(projectRoot, "Moodle.Core/Jsons/course.json");          

            //string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            //var json = System.IO.File.ReadAllText(jsonFilePath);

            //List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);

            ////szures neptunkod szerint
            //List<Course> filteredCourses = courses.Where(c => c.enrolled_students.Contains(neptun)).ToList();

            //string newJson = JsonConvert.SerializeObject(filteredCourses, Formatting.Indented);

            //return this.Content(newJson, "application/json");
        }

        [HttpGet("accepted")]
        public async Task<IActionResult> CheckAcceptedDegrees()
        {
            //string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory

            ////Aktualis felhasznalo degree-jenek lekerese
            //string userData = Path.Combine(projectRoot, "Moodle.Core/Jsons/CurrentUser.json");
            //string userJson = System.IO.File.ReadAllText(userData);
            //dynamic currentUser = JsonConvert.DeserializeObject(userJson);
            //string degree = currentUser["degree"];

            ////Kurzusok kigyujtese
            //string jsonFilePath = Path.Combine(projectRoot, "Moodle.Core/Jsons/course.json");

            //string jsonData = System.IO.File.ReadAllText(jsonFilePath);

            //var json = System.IO.File.ReadAllText(jsonFilePath);

            //List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);

            ////szures degree szerint
            //List<Course> filteredCourses = courses.Where(c => c.enrolled_students.Contains(degree)).ToList();

            //string newJson = JsonConvert.SerializeObject(filteredCourses, Formatting.Indented);

            //return this.Content(newJson, "application/json");
            return Ok();
        }
    }
}
