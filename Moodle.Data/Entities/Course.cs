﻿
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
        public int[] selectedDegrees { get; set; }       
    }
}
