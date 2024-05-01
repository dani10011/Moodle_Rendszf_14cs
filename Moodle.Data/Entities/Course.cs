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
        public string Department { get; set; }
    }

    public class AddCourse
    {
        public string code { get; set; }
        public string name { get; set; }
        public int credit { get; set;}
        public string department { get; set; }
        public int userId { get; set; }            
    }
}
