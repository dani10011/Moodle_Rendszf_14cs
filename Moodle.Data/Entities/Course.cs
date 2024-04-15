using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit {  get; set; }

        //Navigation
        public ICollection<ApprovedDegree> ApprovedDegrees { get; set; }
        public ICollection<MyCourse> MyCourses { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
