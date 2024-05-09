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

            foreach (var course in myCourses)
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
                if (course.User_Id == id && !courseIDs.Contains(course.Course_Id))
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








        [HttpGet("enrolled")] //egy adott kurzusra járó emberek visszaadása, kapott id alapján szűrés
        public IActionResult Enroll(int id)
        {

            var emberek = context.MyCourses.Where(x => x.Course_Id == id).ToList();
            //Console.WriteLine(emberek.Count);

            List<User> kurzsra_jaro_emberek = new List<User>();
            //Console.WriteLine(Cour.id);
            foreach (var ember in emberek)
            {

                var szemely = context.Users.First(x => x.Id == ember.User_Id);
                if (szemely.Role != "tanár") // tanárok kiszűrése
                {
                    kurzsra_jaro_emberek.Add(szemely);
                }
            }
            //Console.WriteLine(kurzsra_jaro_emberek.Count);

            var json = JsonConvert.SerializeObject(kurzsra_jaro_emberek, Formatting.Indented);

            return Content(json, "application/json");
        }




        [HttpGet("event")] //visszaad egy specifikus kurzus id-hez tartozó eseményeket
        public IActionResult GetEventsById(int id)
        {
            var esemenyek = context.Events.Where(x => x.Course_Id == id).ToList();

            var json = JsonConvert.SerializeObject(esemenyek, Formatting.Indented);

            return Content(json, "application/json");
        }



        [HttpPost("AddEvent")] //felvesz egy eseményt, megadott event információk alapján
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
                return Ok(new { message = "Sikeres felvitel!" });
            }
        }


        [HttpGet("DeleteEvent")] //kitöröl egy adott id alapján egy event-et
        public IActionResult DeleteEventById(int id)
        {
            Console.WriteLine(id);
            var esemeny = context.Events.SingleOrDefault(predicate => predicate.Id == id);
            context.Events.Remove(esemeny);
            context.SaveChangesAsync();
            Console.WriteLine("sikeres törlés");
            return Ok(new { message = "Sikeres felvitel!" });
        }


        
        [HttpPost("AddCourse")] //új kurzus hozzáadása (tanár)
        public async Task<IActionResult> AddCourse([FromBody] AddCourse courseInfo)
        {
            if (courseInfo == null)
            {
                return BadRequest("Invalid request body");
            }
            else { 
                var newCourse = new Course
                { Code = courseInfo.code, Name = courseInfo.name, Credit = courseInfo.credit, Department = courseInfo.department };
                context.Courses.Add(newCourse);
                await context.SaveChangesAsync();

                var newCourseId = newCourse.Id;
            
                for(int i=0; i < courseInfo.selectedDegrees.Length; i++){
                   context.Approved_Degrees.Add(new ApprovedDegree { Course_Id = newCourseId, Degree_Id = courseInfo.selectedDegrees[i] });
                    await context.SaveChangesAsync();
                }
           
                context.MyCourses.Add(new MyCourse { Course_Id = newCourseId, User_Id = courseInfo.userId });
                await context.SaveChangesAsync();

                return Ok(new { message = "Sikeres felvitel!" });
            }
        }

        [HttpGet("DeleteCourse")] //kitöröl egy adott id alapján egy kurzust
        public IActionResult DeleteCourseById(int id)
        {

            var kurzus = context.Courses.SingleOrDefault(predicate => predicate.Id == id);
            context.Courses.Remove(kurzus);
            var mycourses = context.MyCourses.Where(p => p.Course_Id == id).ToList();
            foreach(var course in mycourses)
            {
                context.MyCourses.Remove(course);
            }
            context.SaveChangesAsync();
            return Ok("Esemény sikeresen törölve");
        }


        
        [HttpPost("NewCourse")] //kurzus felvétele diákoknak
        public async Task<IActionResult> NewCourse([FromBody] NewCourse nCourse)
        {
            if (nCourse == null)
            {
                return BadRequest("Invalid request body");
            }

            var allApproved = context.Approved_Degrees.ToList();
            List<int> accepted = new List<int>();


            foreach (var c in allApproved)
            {
                if (c.Course_Id == nCourse.course_id)
                {
                    accepted.Add(c.Degree_Id);
                }
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == nCourse.user_id);
            bool approve = false;

            foreach (var c in accepted)
            {
                if (c == user.Degree_Id) { approve = true; }

            }

            if (approve == false)
            {
                return Ok(new { message = "Ehhez a tárgyhoz nem megfelelő a szak, amire jársz." });
            }
            else
            {
                context.MyCourses.Add(new MyCourse { Course_Id = nCourse.course_id, User_Id = nCourse.user_id });
                await context.SaveChangesAsync();

                return Ok(new { message = "Sikeres felvétel!" });
            }
        }

        
        [HttpGet("AllDegrees")] //összes degree elküldése a kliensnek
        public IActionResult AllDegrees()
        {
            var degrees = context.Degrees.ToList();

            var json = JsonConvert.SerializeObject(degrees, Formatting.Indented);

            return Content(json, "application/json");


        }
    }
}
