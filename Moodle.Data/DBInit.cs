using Moodle.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Data
{
    public class DBInit
    {
        private readonly MoodleDbContext context;
        public DBInit(MoodleDbContext _context)
        {
            context = _context;
        }
        public async Task Init()
        {
            
            if (!context.Users.Any())
            {
                List<User> users = new List<User>
                {
                    new User { },
                    new User { }
                };
                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            if (!context.Courses.Any())
            {
                List<Course> courses = new List<Course>
                {
                    new Course { },
                    new Course { }
                };
                context.Courses.AddRange(courses);
                await context.SaveChangesAsync();
            }

            if (!context.Degrees.Any())
            {
                List<Degree> degrees = new List<Degree>
                {
                    new Degree { },
                    new Degree { }
                };
                context.Degrees.AddRange(degrees);
                await context.SaveChangesAsync();
            }

            if (!context.ApprovedDegrees.Any())
            {
                List<ApprovedDegree> approvedDegrees = new List<ApprovedDegree>
                {
                    new ApprovedDegree { },
                    new ApprovedDegree { }
                };
                context.ApprovedDegrees.AddRange(approvedDegrees);
                await context.SaveChangesAsync();
            }

            if (!context.MyCourses.Any())
            {
                List<MyCourse> myCourses = new List<MyCourse>
                {
                    new MyCourse { },
                    new MyCourse { }
                };
                context.MyCourses.AddRange(myCourses);
                await context.SaveChangesAsync();
            }

            if (!context.Events.Any())
            {
                List<Event> events = new List<Event>
                {
                    new Event { },
                    new Event { }
                };
                context.Events.AddRange(events);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
