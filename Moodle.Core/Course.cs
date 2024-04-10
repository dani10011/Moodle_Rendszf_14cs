using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Core
{
    public class Course
    {
        public string? name { get; set; }
        public string? code { get; set; }
        public string? department { get; set; }
        public int credit { get; set; }
        public List<string>? approved_degrees { get; set; }
        public List<string>? enrolled_students { get; set; }
    }
}
