using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int Course_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddEvent
    {
        public int course_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
