using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Data.Entities
{
    public class ApprovedDegree
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int DegreeId { get; set; }

        //Navigation
        public Course Course { get; set; }
        public Degree Degree { get; set; }
    }
}
