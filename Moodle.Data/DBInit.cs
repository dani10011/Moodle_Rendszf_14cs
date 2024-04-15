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
        private readonly MoodleDbContext _context;
        public DBInit(MoodleDbContext context)
        {
            _context = context;
        }
        public async Task Init()
        {
            if (!_context.Users.Any())
            {
                await _context.Users.AddAsync(new User());
                await _context.SaveChangesAsync();
            }

            if (!_context.Courses.Any())
            {

                await _context.SaveChangesAsync();
            }

            if (!_context.Degrees.Any())
            {

                await _context.SaveChangesAsync();
            }

            if (!_context.ApprovedDegrees.Any())
            {

                await _context.SaveChangesAsync();
            }

            if (!_context.MyCourses.Any())
            {

                await _context.SaveChangesAsync();
            }

            if (!_context.Events.Any())
            {

                await _context.SaveChangesAsync();
            }
            
        }
    }
}
