

namespace Moodle.Data.Entities
{
    public class MyCourse
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Course_Id { get; set; }
    }

    public class NewCourse 
    { 
        public int user_id { get; set; }
        public int course_id { get; set; }
     
    }
}
