
namespace Moodle.Data.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string UserName { get; set; } // Neptun kód
        public string Name { get; set; } //Valós név
        public string Password { get; set; }
        public int Degree_Id { get; set; }
        public string Role { get; set; }

    }
}
