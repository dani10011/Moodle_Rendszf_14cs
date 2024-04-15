using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Data.Entities
{
    public class MyCourse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        //Navigation
        public User User { get; set; }
        public Course Course { get; set; }
    }
}
