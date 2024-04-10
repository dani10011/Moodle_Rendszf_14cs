using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Core
{
    public class User
    {
        public string? Name { get; set; }
        public string? Neptun_code { get; set; }
        public string? Degree { get; set; }
        public virtual List<Course>? Courses { get; set; }
    }
}
