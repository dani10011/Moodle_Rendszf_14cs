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

        [HttpGet("allcourses")] //összes kurzust visszaküldi a kliensnek
        public IActionResult GetAllCourses()
        {
            var courses = context.Courses.ToList(); // Retrieve all courses from the database

            var json = JsonConvert.SerializeObject(courses, Formatting.Indented); // Serialize courses to JSON

            return Content(json, "application/json"); // Return JSON content

        }

        [HttpGet("courseid")]   //kurzusokat kilistázza egy adott emberhez, egy kapott ID alapján
        public IActionResult GetCoursesByID(int id)
        {

            var myCourses = context.MyCourses.ToList();

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

        }


        [HttpGet("notincourseid")]  //kilistázza azokat a kurzusokat amin az illető nincs, kapott id alapján
        public IActionResult GetNotInCoursesByID(int id)
        {

            var myCourses = context.MyCourses.ToList();

            var courses = context.Courses.ToList();

            List<int> courseIDs = new List<int>();

            foreach (var course in myCourses)
            {
                if (course.User_Id == id)
                {
                    courseIDs.Add(course.Course_Id);
                }
            }

            List<Course> uCourses = courses;

            foreach (int i in courseIDs)
            {
                var course = courses.First(x => x.Id == i);
                uCourses.Remove(course);
            }
            //var uCourses = courses.Where(c => c.Id.Equals(courseIDs)).ToList();

            var json = JsonConvert.SerializeObject(uCourses, Formatting.Indented);

            return Content(json, "application/json");

        }





        /*
        [HttpPost("accepted")]
        public async Task<IActionResult> CheckAcceptedDegrees([FromBody] int courseID)
        {
            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory

            //Aktualis felhasznalo idjanak lekerese
            string userData = Path.Combine(projectRoot, "Moodle.Core/Jsons/CurrentUser.json");
            string userJson = System.IO.File.ReadAllText(userData);
            dynamic currentUser = JsonConvert.DeserializeObject(userJson);
            int id = currentUser["ID"];

            var users = context.Users.ToList();

            User user = users.Where(u => u.Id == id).FirstOrDefault();

            List<MyCourse> courses = context.MyCourses.Where(c => c.Course_Id.Equals(courseID)).ToList();

            bool ok = false;
            foreach (var course in courses)
            {
                if (user.Degree_Id == courseID)
                { 
                    ok = true; 
                    break; 
                }
            }
            if (ok)
            {
                return Ok("Sikeres feliratkozás!");
            }
            else
            {
                return BadRequest("Nem megfelelő szakra jár!");
            }
        
        }
        */


        [HttpGet("enrolled")] //egy adott kurzusra járó emberek visszaadása, kapott id alapján szűrés
        public IActionResult Enroll(int id)
        {
            
            var emberek = context.MyCourses.Where(x => x.Course_Id == id).ToList();
            //Console.WriteLine(emberek.Count);

            List<User> kurzsra_jaro_emberek = new List<User>();
            //Console.WriteLine(Course_Id.id);
            foreach (var ember in emberek)
            {
                var szemely = context.Users.First(x => x.Id == ember.User_Id);
                kurzsra_jaro_emberek.Add(szemely);
            }
            //Console.WriteLine(kurzsra_jaro_emberek.Count);

            var json = JsonConvert.SerializeObject(kurzsra_jaro_emberek, Formatting.Indented);

            return Content(json, "application/json");
        }




        [HttpGet("event")] //visszaad egy specifikus kurzus id-hez tartozó eseményeket
        public IActionResult GetEventsById(int id)
        {
            var esemenyek = context.Events.Where(x =>x.Course_Id == id).ToList();

            var json = JsonConvert.SerializeObject(esemenyek, Formatting.Indented);

            return Content(json, "application/json");
        }




        [HttpPost("AddEvent")]
        public async Task<IActionResult> AddEvent([FromBody] AddEvent eventInfo)
        {
            if (eventInfo == null)
            {
                return BadRequest("Invalid request body");
            }
            else
            {
                Console.WriteLine(eventInfo.description);
                context.Events.Add(new Event { Course_Id = eventInfo.course_id, Name = eventInfo.name, Description = eventInfo.description });
                await context.SaveChangesAsync();
                return Ok(new { message = "Sikeres felvétel!" });
            }
        }





    }
}
