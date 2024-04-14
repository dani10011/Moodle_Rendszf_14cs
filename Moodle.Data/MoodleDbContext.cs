using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moodle.Core;

namespace Moodle.Data
{
    
    public class MoodleDbContext : DbContext
    {

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).FullName; // Get project root directory
            string loginInfoPath = Path.Combine(projectRoot, "Moodle.Core/Databases/Moodle_adatbazisok.mdf");

            optionsBuilder.UseSqlServer(@"Data Source=.\\SQLEXPRESS;AttachDbFilename=" + loginInfoPath + ";Integrated Security=True;Connect Timeout=30"); // Replace with your connection string
        }
    }
    
}
