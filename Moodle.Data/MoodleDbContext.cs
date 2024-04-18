using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moodle.Data.Entities;

namespace Moodle.Data
{
    
    public class MoodleDbContext : DbContext
    {
        //public MoodleDbContext() { }
        public MoodleDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<ApprovedDegree> Approved_Degrees { get; set; }
        public DbSet<MyCourse> MyCourses { get; set; }
        public DbSet<Event> Events { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory
            string loginInfoPath = Path.Combine(projectRoot, "Moodle.Data/Databases/MoodleDB.db");

            //optionsBuilder.UseSqlServer(@"Data Source=.\\MoodleDB;AttachDbFilename=" + loginInfoPath + ";Integrated Security=True;Connect Timeout=30"); // Replace with your connection string
            optionsBuilder.UseSqlite(@"Data Source = " + loginInfoPath);
        }
    }
    
}
