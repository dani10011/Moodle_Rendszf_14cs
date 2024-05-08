using Microsoft.EntityFrameworkCore;
using Moodle.Data.Entities;


namespace Moodle.Data
{
    public class DBInit
    {
        private readonly MoodleDbContext context;
        public DBInit(MoodleDbContext _context)
        {
            context = _context;
        }

        public async Task Wipe()
        {
            // Disable foreign key checks to avoid cascading deletes failing
            await context.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = OFF;");

            // Get a list of all tables using reflection
            var tables = context.Model.GetEntityTypes().Select(et => et.GetTableName()).ToList();

            // Delete data from each table
            foreach (var table in tables)
            {
                await context.Database.ExecuteSqlRawAsync($"DELETE FROM {table}");
            }

            // Re-enable foreign key checks
            await context.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = ON;");
        }
        public async Task Init()
        {

            if (!context.Degrees.Any())
            {
                List<Degree> degrees = new List<Degree>
                {
                    new Degree { Id = 1, Name = "Programtervező Informatikus BSc"},
                    new Degree { Id = 2, Name = "Mérnök Informatikus BSc"},
                    new Degree { Id = 3, Name = "Gazadsági Informatikus BSc"},
                    new Degree { Id = 4, Name = "Üzemmérnök Informatikus BSc"},
                    new Degree { Id = 5, Name = "Gazdálkodás és Menedzsment BSc"},
                    new Degree { Id = 6, Name = "Anglisztika BA"},
                    new Degree { Id = 7, Name = "Villamosmérnök BSc"},
                    new Degree { Id = 8, Name = "Anyagmérnöki BSc"},
                    new Degree { Id = 9, Name = "Emberi erőforrás BSc"},
                    new Degree { Id = 10, Name = "Gépészmérnöki BSc"},
                    new Degree { Id = 11, Name = "Kémia BSc"},
                    new Degree { Id = 12, Name = "Tanár"}
                };
                context.Degrees.AddRange(degrees);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                List<User> users = new List<User>
                {
                    new User { Id = 1, UserName = "H8UOA6", Name = "Kovács L. Bendegúz", Password = "1234", Degree_Id = 1, Role = "diák"},
                    new User { Id = 2, UserName = "B7FY4E", Name = "Virágh Áron", Password = "12345", Degree_Id = 1, Role = "diák"},
                    new User { Id = 3, UserName = "CT3QB5", Name = "Ökrös Dániel", Password = "Jelszo1", Degree_Id = 1, Role = "diák"},
                    new User { Id = 4, UserName = "URK2SP", Name = "Kovács Bence", Password = "ucw803", Degree_Id = 1, Role = "diák"},
                    new User { Id = 5, UserName = "A9T32P", Name = "Bálint Miklós", Password = "nagyafaszom", Degree_Id = 3, Role = "diák"},
                    new User { Id = 6, UserName = "XG739D", Name = "Minta Béla", Password = "mintajel", Degree_Id = 4, Role = "diák"},
                    new User { Id = 7, UserName = "FAR0K5", Name = "Péter Árpád", Password = "titkos01", Degree_Id = 7, Role = "diák"},
                    new User { Id = 8, UserName = "OQNZ3B", Name = "Juhász Zoltán", Password = "java8", Degree_Id = 12, Role = "tanár"},
                    new User { Id = 9, UserName = "2F84HR", Name = "Frits Márton", Password = "kreatin", Degree_Id = 12, Role = "tanár"},
                    new User { Id = 10, UserName = "VQB9U4", Name = "Tarczali Tünde", Password = "ViAron04", Degree_Id = 12, Role = "tanár"},
                    new User { Id = 11, UserName = "AR4K1S", Name = "Paul Atreides", Password = "LisanAlGaib", Degree_Id = 2, Role = "diák"},
                    new User { Id = 12, UserName = "GH2FG7", Name = "Horváth Ádám", Password = "HAdam101", Degree_Id = 12, Role = "tanár"}

                };

                
                List<User> hashedUsers = Hashing.HashAllUserPasswords(users);

                context.Users.AddRange(hashedUsers);
                await context.SaveChangesAsync();
            }

            if (!context.Courses.Any())
            {
                List<Course> courses = new List<Course>
                {
                    new Course { Id = 1, Code = "VEMISAB223RF", Name = "A rendszerfejlesztés haladó módszerei", Credit = 3, Department = "RSZT"},
                    new Course { Id = 2, Code = "VEMIVIB343BL", Name = "Bevezetés a lágy számítás módszereibe", Credit = 3, Department = "VIRT"},
                    new Course { Id = 3, Code = "VEMISAB154RF", Name = "Rendszertesztelés", Credit = 4, Department = "RSZT"},
                    new Course { Id = 4, Code = "VEMKFISV12K", Name = "A világegyetem megismerésének története", Credit = 2, Department = "TTK"},
                    new Course { Id = 5, Code = "VEMISAB144SV", Name = "Szoftvertechnológia", Credit = 4, Department = "RSZT"},
                    new Course { Id = 6, Code = "VEMIMAB144AF", Name = "Alkalmazott statisztika", Credit = 4, Department = "MAT"},
                    new Course { Id = 7, Code = "VEMKSVKA12H", Name = "Honvédelmi alapismeretek", Credit = 2, Department = "TTK"},                    
                    new Course { Id = 8, Code = "VEMIVIB146JF", Name = "Java programozás I.", Credit = 6, Department = "VIRT"},
                    new Course { Id = 9, Code = "VEMISAB254ZF", Name = "Python programozás", Credit = 4, Department = "RSZT"},
                    new Course { Id = 10, Code = "VEMISAB122E", Name = "Elemi algoritmusok", Credit = 3, Department = "RSZT"},
                    new Course { Id = 11, Code = "VEMIKNB113A", Name = "Számítógép -architektúrák", Credit = 3, Department = "VIRT"},
                    new Course { Id = 12, Code = "VETKMA1243D", Name = "Diszkrét matematika", Credit = 3, Department = "MAT"}
                };
                context.Courses.AddRange(courses);
                await context.SaveChangesAsync();
            }

            if (!context.Approved_Degrees.Any())
            {
                List<ApprovedDegree> approvedDegrees = new List<ApprovedDegree>
                {
                    //RendszF
                    new ApprovedDegree { Id = 1, Course_Id = 1, Degree_Id = 1 },
                    new ApprovedDegree { Id = 2, Course_Id = 1, Degree_Id = 2 },
                    new ApprovedDegree { Id = 3, Course_Id = 1, Degree_Id = 3 },
                    new ApprovedDegree { Id = 4, Course_Id = 1, Degree_Id = 4 },
                    new ApprovedDegree { Id = 5, Course_Id = 1, Degree_Id = 12 },
                    //Lágysz
                    new ApprovedDegree { Id = 6, Course_Id = 2, Degree_Id = 1 },
                    new ApprovedDegree { Id = 7, Course_Id = 2, Degree_Id = 2 },
                    new ApprovedDegree { Id = 8, Course_Id = 2, Degree_Id = 3 },
                    new ApprovedDegree { Id = 9, Course_Id = 2, Degree_Id = 4 },
                    new ApprovedDegree { Id = 10, Course_Id = 2, Degree_Id = 12 },
                    //RendszT
                    new ApprovedDegree { Id = 11, Course_Id = 3, Degree_Id = 1 },
                    new ApprovedDegree { Id = 12, Course_Id = 3, Degree_Id = 2 },
                    new ApprovedDegree { Id = 13, Course_Id = 3, Degree_Id = 3 },
                    new ApprovedDegree { Id = 14, Course_Id = 3, Degree_Id = 4 },
                    new ApprovedDegree { Id = 15, Course_Id = 3, Degree_Id = 12 },
                    //Világ
                    new ApprovedDegree { Id = 16, Course_Id = 4, Degree_Id = 1 },
                    new ApprovedDegree { Id = 17, Course_Id = 4, Degree_Id = 2 },
                    new ApprovedDegree { Id = 18, Course_Id = 4, Degree_Id = 3 },
                    new ApprovedDegree { Id = 19, Course_Id = 4, Degree_Id = 4 },
                    new ApprovedDegree { Id = 20, Course_Id = 4, Degree_Id = 5 },
                    new ApprovedDegree { Id = 21, Course_Id = 4, Degree_Id = 6 },
                    new ApprovedDegree { Id = 22, Course_Id = 4, Degree_Id = 7 },
                    new ApprovedDegree { Id = 23, Course_Id = 4, Degree_Id = 8 },
                    new ApprovedDegree { Id = 24, Course_Id = 4, Degree_Id = 9 },
                    new ApprovedDegree { Id = 25, Course_Id = 4, Degree_Id = 10 },
                    new ApprovedDegree { Id = 26, Course_Id = 4, Degree_Id = 11 },
                    new ApprovedDegree { Id = 27, Course_Id = 4, Degree_Id = 12 },
                    //Szofttech
                    new ApprovedDegree { Id = 28, Course_Id = 5, Degree_Id = 1 },
                    new ApprovedDegree { Id = 29, Course_Id = 5, Degree_Id = 2 },
                    new ApprovedDegree { Id = 30, Course_Id = 5, Degree_Id = 3 },
                    new ApprovedDegree { Id = 31, Course_Id = 5, Degree_Id = 4 },
                    new ApprovedDegree { Id = 32, Course_Id = 5, Degree_Id = 12 },
                    //Stat
                    new ApprovedDegree { Id = 33, Course_Id = 6, Degree_Id = 1 },
                    new ApprovedDegree { Id = 34, Course_Id = 6, Degree_Id = 2 },
                    new ApprovedDegree { Id = 35, Course_Id = 6, Degree_Id = 3 },
                    new ApprovedDegree { Id = 36, Course_Id = 6, Degree_Id = 4 },
                    new ApprovedDegree { Id = 37, Course_Id = 6, Degree_Id = 12 },
                    //Honv
                    new ApprovedDegree { Id = 38, Course_Id = 7, Degree_Id = 1 },
                    new ApprovedDegree { Id = 39, Course_Id = 7, Degree_Id = 2 },
                    new ApprovedDegree { Id = 40, Course_Id = 7, Degree_Id = 3 },
                    new ApprovedDegree { Id = 41, Course_Id = 7, Degree_Id = 4 },
                    new ApprovedDegree { Id = 42, Course_Id = 7, Degree_Id = 5 },
                    new ApprovedDegree { Id = 43, Course_Id = 7, Degree_Id = 6 },
                    new ApprovedDegree { Id = 44, Course_Id = 7, Degree_Id = 7 },
                    new ApprovedDegree { Id = 45, Course_Id = 7, Degree_Id = 8 },
                    new ApprovedDegree { Id = 46, Course_Id = 7, Degree_Id = 9 },
                    new ApprovedDegree { Id = 47, Course_Id = 7, Degree_Id = 10 },
                    new ApprovedDegree { Id = 48, Course_Id = 7, Degree_Id= 11 },
                    new ApprovedDegree { Id = 49, Course_Id = 7, Degree_Id = 12 },
                    //Java
                    new ApprovedDegree { Id = 50, Course_Id = 8, Degree_Id = 1 },
                    new ApprovedDegree { Id = 51, Course_Id = 8, Degree_Id = 2 },
                    new ApprovedDegree { Id = 52, Course_Id = 8, Degree_Id = 3 },
                    new ApprovedDegree { Id = 53, Course_Id = 8, Degree_Id = 4 },
                    new ApprovedDegree { Id = 54, Course_Id = 8, Degree_Id = 12 },
                    //Py
                    new ApprovedDegree { Id = 55, Course_Id = 9, Degree_Id = 1 },
                    new ApprovedDegree { Id = 56, Course_Id = 9, Degree_Id = 2 },
                    new ApprovedDegree { Id = 57, Course_Id = 9, Degree_Id = 3 },
                    new ApprovedDegree { Id = 58, Course_Id = 9, Degree_Id = 4 },
                    new ApprovedDegree { Id = 59, Course_Id = 9, Degree_Id = 12 },
                    //ElAlg
                    new ApprovedDegree { Id = 60, Course_Id = 10, Degree_Id = 1 },
                    new ApprovedDegree { Id = 61, Course_Id = 10, Degree_Id = 2 },
                    new ApprovedDegree { Id = 62, Course_Id = 10, Degree_Id = 3 },
                    new ApprovedDegree { Id = 63, Course_Id = 10, Degree_Id = 4 },
                    new ApprovedDegree { Id = 64, Course_Id = 10, Degree_Id = 7 },
                    new ApprovedDegree { Id = 65, Course_Id = 10, Degree_Id = 12 },
                    //SzAr
                    new ApprovedDegree { Id = 66, Course_Id = 11, Degree_Id = 1 },
                    new ApprovedDegree { Id = 67, Course_Id = 11, Degree_Id = 2 },
                    new ApprovedDegree { Id = 68, Course_Id = 11, Degree_Id = 3 },
                    new ApprovedDegree { Id = 69, Course_Id = 11, Degree_Id = 4 },
                    new ApprovedDegree { Id = 70, Course_Id = 11, Degree_Id = 12 },
                    //Dimat
                    new ApprovedDegree { Id = 71, Course_Id = 12, Degree_Id = 1 },
                    new ApprovedDegree { Id = 72, Course_Id = 12, Degree_Id = 2 },
                    new ApprovedDegree { Id = 73, Course_Id = 12, Degree_Id = 3 },
                    new ApprovedDegree { Id = 74, Course_Id = 12, Degree_Id = 4 },
                    new ApprovedDegree { Id = 75, Course_Id = 12, Degree_Id = 12 }
                };
                context.Approved_Degrees.AddRange(approvedDegrees);
                await context.SaveChangesAsync();
            }

            if (!context.MyCourses.Any())
            {
                List<MyCourse> myCourses = new List<MyCourse>
                {
                    //Tanárok hozzárendelése kurzusokhoz
                    new MyCourse { Id = 1, User_Id = 9, Course_Id = 1 },
                    new MyCourse { Id = 2, User_Id = 10, Course_Id = 2 },
                    new MyCourse { Id = 3, User_Id = 9, Course_Id = 3 },
                    new MyCourse { Id = 4, User_Id = 10, Course_Id = 4 },
                    new MyCourse { Id = 5, User_Id = 10, Course_Id = 5 },
                    new MyCourse { Id = 6, User_Id = 12, Course_Id = 6 },
                    new MyCourse { Id = 7, User_Id = 8, Course_Id = 7 },
                    new MyCourse { Id = 8, User_Id = 8, Course_Id = 8 },
                    new MyCourse { Id = 9, User_Id = 9, Course_Id = 9 },
                    new MyCourse { Id = 10, User_Id = 12, Course_Id = 10 },
                    new MyCourse { Id = 11, User_Id = 8, Course_Id = 11 },
                    new MyCourse { Id = 12, User_Id = 12, Course_Id = 12 },
                    //Diákok hozzárendelése a kurzusokhoz
                    new MyCourse { Id = 13, User_Id = 1, Course_Id = 8 },
                    new MyCourse { Id = 14, User_Id = 1, Course_Id = 11 },
                    new MyCourse { Id = 15, User_Id = 2, Course_Id = 4 },
                    new MyCourse { Id = 16, User_Id = 2, Course_Id = 5 },
                    new MyCourse { Id = 17, User_Id = 2, Course_Id = 2 },
                    new MyCourse { Id = 18, User_Id = 3, Course_Id = 2 },
                    new MyCourse { Id = 19, User_Id = 3, Course_Id = 4 },
                    new MyCourse { Id = 20, User_Id = 5, Course_Id = 8 },
                    new MyCourse { Id = 21, User_Id = 5, Course_Id = 9 },
                    new MyCourse { Id = 22, User_Id = 7, Course_Id = 4 },
                    new MyCourse { Id = 23, User_Id = 7, Course_Id = 7 },
                    //Minden kurzusra járó emberek: 4, 6, 7, 11
                    new MyCourse { Id = 24, User_Id = 4, Course_Id = 1 },
                    new MyCourse { Id = 25, User_Id = 4, Course_Id = 2 },
                    new MyCourse { Id = 26, User_Id = 4, Course_Id = 3 },
                    new MyCourse { Id = 27, User_Id = 4, Course_Id = 4 },
                    new MyCourse { Id = 28, User_Id = 4, Course_Id = 5 },
                    new MyCourse { Id = 29, User_Id = 4, Course_Id = 6 },
                    new MyCourse { Id = 30, User_Id = 4, Course_Id = 7 },
                    new MyCourse { Id = 31, User_Id = 4, Course_Id = 8 },
                    new MyCourse { Id = 32, User_Id = 4, Course_Id = 9 },
                    new MyCourse { Id = 33, User_Id = 4, Course_Id = 10 },
                    new MyCourse { Id = 34, User_Id = 4, Course_Id = 11 },
                    new MyCourse { Id = 35, User_Id = 4, Course_Id = 12 },

                    new MyCourse { Id = 36, User_Id = 6, Course_Id = 1 },
                    new MyCourse { Id = 37, User_Id = 6, Course_Id = 2 },
                    new MyCourse { Id = 38, User_Id = 6, Course_Id = 3 },
                    new MyCourse { Id = 39, User_Id = 6, Course_Id = 4 },
                    new MyCourse { Id = 40, User_Id = 6, Course_Id = 5 },
                    new MyCourse { Id = 41, User_Id = 6, Course_Id = 6 },
                    new MyCourse { Id = 42, User_Id = 6, Course_Id = 7 },
                    new MyCourse { Id = 43, User_Id = 6, Course_Id = 8 },
                    new MyCourse { Id = 44, User_Id = 6, Course_Id = 9 },
                    new MyCourse { Id = 45, User_Id = 6, Course_Id = 10 },
                    new MyCourse { Id = 46, User_Id = 6, Course_Id = 11 },
                    new MyCourse { Id = 47, User_Id = 6, Course_Id = 12 },

                    new MyCourse { Id = 48, User_Id = 11, Course_Id = 1 },
                    new MyCourse { Id = 49, User_Id = 11, Course_Id = 2 },
                    new MyCourse { Id = 50, User_Id = 11, Course_Id = 3 },
                    new MyCourse { Id = 51, User_Id = 11, Course_Id = 4 },
                    new MyCourse { Id = 52, User_Id = 11, Course_Id = 5 },
                    new MyCourse { Id = 53, User_Id = 11, Course_Id = 6 },
                    new MyCourse { Id = 54, User_Id = 11, Course_Id = 7 },
                    new MyCourse { Id = 55, User_Id = 11, Course_Id = 8 },
                    new MyCourse { Id = 56, User_Id = 11, Course_Id = 9 },
                    new MyCourse { Id = 57, User_Id = 11, Course_Id = 10 },
                    new MyCourse { Id = 58, User_Id = 11, Course_Id = 11 },
                    new MyCourse { Id = 59, User_Id = 11, Course_Id = 12 },

                };
                context.MyCourses.AddRange(myCourses);
                await context.SaveChangesAsync();
            }

            if (!context.Events.Any())
            {
                List<Event> events = new List<Event>
                {
                    new Event { Id = 1, Course_Id = 1, Name = "Második beadandó leadása", Description = "A szerver oldal bővítése adatbázis kezeléssel" },
                    new Event { Id = 2, Course_Id = 2, Name = "Genetikus algoritmusok, 1. dolgozat", Description = "A dolgozat megírásához szüksége lesz a MATLAB Global Optimization Toolbox-ára" },
                    new Event { Id = 3, Course_Id = 3, Name = "3. Labor: Tesztelési jegyzőkönyv feltöltése", Description = "Software Fault Injection tesztelés" },
                    new Event { Id = 4, Course_Id = 4, Name = "Év végi zárthelyi dolgozat feltöltése", Description = "Beszámoló a világegyetem megismerése során szerzett ismeretekről" },
                    new Event { Id = 5, Course_Id = 5, Name = "Elméleti zárthelyi", Description = "A 143 kérdésből 10 sorsolt kérdésből álló teszt" },
                    new Event { Id = 6, Course_Id = 6, Name = "2. zárthelyi dolgozat", Description = "Összetett konzol alkalmazás készítése" },
                    new Event { Id = 7, Course_Id = 7, Name = "Év végi zárthelyi teszt", Description = "A haza védelme" },
                    new Event { Id = 8, Course_Id = 8, Name = "1. zárthelyi dolgozat", Description = "Valószínűség számítás számonkérés" },
                    new Event { Id = 9, Course_Id = 9, Name = "4. kZh", Description = "MongoDB kisZh" },
                    new Event { Id = 10, Course_Id = 10, Name = "1. zh eredmények", Description = "Horváth Ádám csoportja (Cs 14-16)" },
                    new Event { Id = 11, Course_Id = 11, Name = "Amit a héten át kell nézni", Description = "Utasítások, címzési módok, RISC/CISC processzorok" },
                    new Event { Id = 12, Course_Id = 12, Name = "6.6 Tétel", Description = "Tétel megtanulása és felmondása"}
                };
                context.Events.AddRange(events);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
