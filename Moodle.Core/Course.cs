using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Core
{
    public class Course
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Department { get; set; }
        public int Credit { get; set; }
        public List<string> ApprovedDegrees { get; set; }
        public List<string> EnrolledStudents { get; set; }
    }
}
