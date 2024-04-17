using Moodle.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Data
{
    public class DBInit
    {
        private readonly MoodleDbContext context;
        public DBInit(MoodleDbContext _context)
        {
            context = _context;
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
                    new Degree { Id = 11, Name = "Kémia BSc"}
                };
                context.Degrees.AddRange(degrees);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                List<User> users = new List<User>
                {
                    new User { Id = 1, UserName = "H8UOA6", Name = "Kovács L. Bendegúz", Password = "1234", DegreeId = 1},
                    new User { Id = 2, UserName = "B7FY4E", Name = "Virágh Áron", Password = "12345", DegreeId = 1},
                    new User { Id = 3, UserName = "CT3QB5", Name = "Ökrös Dániel", Password = "Jelszo1", DegreeId = 1},
                    new User { Id = 4, UserName = "URK2SP", Name = "Kovács Bence", Password = "ucw803", DegreeId = 6},
                    new User { Id = 5, UserName = "A9T32P", Name = "Bálint Miklós", Password = "nagyafaszom", DegreeId = 3},
                    new User { Id = 6, UserName = "FAROK5", Name = "Péter Árpád", Password = "titkos01", DegreeId = 7},
                    new User { Id = 7, UserName = "XG739D", Name = "Minta Béla", Password = "mintajel", DegreeId = 8},
                    new User { Id = 8, UserName = "OQNZ3B", Name = "Juhász Zoltán", Password = "java8", DegreeId = 4},
                    new User { Id = 9, UserName = "2F84HR", Name = "Frits Márton", Password = "kreatin", DegreeId = 2},
                    new User { Id = 10, UserName = "VQB9U4", Name = "Tarczali Tünde", Password = "ViAron04", DegreeId = 5},
                    new User { Id = 11, UserName = "AR4K1S", Name = "Paul Atreides", Password = "LisanAlGaib", DegreeId = 9}

                };
                context.Users.AddRange(users);
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
                    new Course { Id = 6, Code = "VEMIVIB146JF", Name = "Java programozás I.", Credit = 6, Department = "VIRT"},
                    new Course { Id = 7, Code = "VEMKSVKA12H", Name = "Honvédelmi alapismeretek", Credit = 2, Department = "TTK"},
                    new Course { Id = 8, Code = "VEMIMAB144AF", Name = "Alkalmazott statisztika", Credit = 4, Department = "MAT"},
                    new Course { Id = 9, Code = "VEMISAB254ZF", Name = "Python programozás", Credit = 4, Department = "RSZT"},
                    new Course { Id = 10, Code = "VEMISAB122E", Name = "Elemi algoritmusok", Credit = 3, Department = "RSZT"},
                    new Course { Id = 11, Code = "VEMIKNB113A", Name = "Számítógép -architektúrák", Credit = 3, Department = "VIRT"}
                };
                context.Courses.AddRange(courses);
                await context.SaveChangesAsync();
            }

            if (!context.ApprovedDegrees.Any())
            {
                List<ApprovedDegree> approvedDegrees = new List<ApprovedDegree>
                {
                    new ApprovedDegree { Id = 1, CourseId = 1, DegreeId = 1 },
                    new ApprovedDegree { Id = 2, CourseId = 1, DegreeId = 2 },
                    new ApprovedDegree { Id = 3, CourseId = 1, DegreeId = 3 },
                    new ApprovedDegree { Id = 4, CourseId = 1, DegreeId = 4 },

                    new ApprovedDegree { Id = 5, CourseId = 2, DegreeId = 1 },
                    new ApprovedDegree { Id = 6, CourseId = 2, DegreeId = 2 },
                    new ApprovedDegree { Id = 7, CourseId = 2, DegreeId = 3 },
                    new ApprovedDegree { Id = 8, CourseId = 2, DegreeId = 4 },

                    new ApprovedDegree { Id = 9, CourseId = 3, DegreeId = 1 },
                    new ApprovedDegree { Id = 10, CourseId = 3, DegreeId = 2 },
                    new ApprovedDegree { Id = 11, CourseId = 3, DegreeId = 3 },
                    new ApprovedDegree { Id = 12, CourseId = 3, DegreeId = 4 },

                    new ApprovedDegree { Id = 13, CourseId = 4, DegreeId = 1 },
                    new ApprovedDegree { Id = 14, CourseId = 4, DegreeId = 2 },
                    new ApprovedDegree { Id = 15, CourseId = 4, DegreeId = 3 },
                    new ApprovedDegree { Id = 16, CourseId = 4, DegreeId = 4 },
                    new ApprovedDegree { Id = 17, CourseId = 4, DegreeId = 5 },
                    new ApprovedDegree { Id = 18, CourseId = 4, DegreeId = 6 },
                    new ApprovedDegree { Id = 19, CourseId = 4, DegreeId = 7 },
                    new ApprovedDegree { Id = 20, CourseId = 4, DegreeId = 8 },
                    new ApprovedDegree { Id = 21, CourseId = 4, DegreeId = 9 },
                    new ApprovedDegree { Id = 22, CourseId = 4, DegreeId = 10 },
                    new ApprovedDegree { Id = 23, CourseId = 4, DegreeId = 11 },

                    new ApprovedDegree { Id = 24, CourseId = 5, DegreeId = 1 },
                    new ApprovedDegree { Id = 25, CourseId = 5, DegreeId = 2 },
                    new ApprovedDegree { Id = 26, CourseId = 5, DegreeId = 3 },
                    new ApprovedDegree { Id = 27, CourseId = 5, DegreeId = 4 },

                    new ApprovedDegree { Id = 28, CourseId = 6, DegreeId = 1 },
                    new ApprovedDegree { Id = 29, CourseId = 6, DegreeId = 2 },
                    new ApprovedDegree { Id = 30, CourseId = 6, DegreeId = 3 },
                    new ApprovedDegree { Id = 31, CourseId = 6, DegreeId = 4 },

                    new ApprovedDegree { Id = 32, CourseId = 7, DegreeId = 1 },
                    new ApprovedDegree { Id = 33, CourseId = 7, DegreeId = 2 },
                    new ApprovedDegree { Id = 34, CourseId = 7, DegreeId = 3 },
                    new ApprovedDegree { Id = 35, CourseId = 7, DegreeId = 4 },
                    new ApprovedDegree { Id = 36, CourseId = 7, DegreeId = 5 },
                    new ApprovedDegree { Id = 37, CourseId = 7, DegreeId = 6 },
                    new ApprovedDegree { Id = 38, CourseId = 7, DegreeId = 7 },
                    new ApprovedDegree { Id = 39, CourseId = 7, DegreeId = 8 },
                    new ApprovedDegree { Id = 40, CourseId = 7, DegreeId = 9 },
                    new ApprovedDegree { Id = 41, CourseId = 7, DegreeId = 10 },

                    new ApprovedDegree { Id = 42, CourseId = 8, DegreeId = 1 },
                    new ApprovedDegree { Id = 43, CourseId = 8, DegreeId = 2 },
                    new ApprovedDegree { Id = 44, CourseId = 8, DegreeId = 3 },
                    new ApprovedDegree { Id = 45, CourseId = 8, DegreeId = 4 },

                    new ApprovedDegree { Id = 46, CourseId = 9, DegreeId = 1 },
                    new ApprovedDegree { Id = 47, CourseId = 9, DegreeId = 2 },
                    new ApprovedDegree { Id = 48, CourseId = 9, DegreeId = 3 },
                    new ApprovedDegree { Id = 49, CourseId = 9, DegreeId = 4 },

                    new ApprovedDegree { Id = 50, CourseId = 10, DegreeId = 1 },
                    new ApprovedDegree { Id = 51, CourseId = 10, DegreeId = 2 },
                    new ApprovedDegree { Id = 52, CourseId = 10, DegreeId = 3 },
                    new ApprovedDegree { Id = 53, CourseId = 10, DegreeId = 4 },
                    new ApprovedDegree { Id = 54, CourseId = 10, DegreeId = 7 }
                };
                context.ApprovedDegrees.AddRange(approvedDegrees);
                await context.SaveChangesAsync();
            }

            if (!context.MyCourses.Any())
            {
                List<MyCourse> myCourses = new List<MyCourse>
                {
                    new MyCourse { Id = 1, UserId = 1, CourseId = 8 },
                    new MyCourse { Id = 2, UserId = 1, CourseId = 11 },
                    new MyCourse { Id = 3, UserId = 2, CourseId = 5 },
                    new MyCourse { Id = 4, UserId = 2, CourseId = 6 },
                    new MyCourse { Id = 5, UserId = 3, CourseId = 3 },
                    new MyCourse { Id = 6, UserId = 3, CourseId = 2 },
                    new MyCourse { Id = 7, UserId = 4, CourseId = 4 },
                    new MyCourse { Id = 8, UserId = 4, CourseId = 7 },
                    new MyCourse { Id = 9, UserId = 5, CourseId = 9 },
                    new MyCourse { Id = 10, UserId = 5, CourseId = 11 },
                    new MyCourse { Id = 11, UserId = 6, CourseId = 4 },
                    new MyCourse { Id = 12, UserId = 6, CourseId = 7 },
                    new MyCourse { Id = 13, UserId = 7, CourseId = 4 },
                    new MyCourse { Id = 14, UserId = 7, CourseId = 7 },
                    new MyCourse { Id = 15, UserId = 8, CourseId = 6 },
                    new MyCourse { Id = 16, UserId = 8, CourseId = 9 },
                    new MyCourse { Id = 17, UserId = 9, CourseId = 1 },
                    new MyCourse { Id = 18, UserId = 9, CourseId = 9 },
                    new MyCourse { Id = 19, UserId = 10, CourseId = 4 },
                    new MyCourse { Id = 20, UserId = 10, CourseId = 7 },
                    new MyCourse { Id = 21, UserId = 11, CourseId = 4 },
                    new MyCourse { Id = 22, UserId = 11, CourseId = 7 },
                };
                context.MyCourses.AddRange(myCourses);
                await context.SaveChangesAsync();
            }

            if (!context.Events.Any())
            {
                List<Event> events = new List<Event>
                {
                    new Event { Id = 1, CourseId = 1, Name = "Második beadandó leadása", Description = "A szerver oldal bővítése adatbázis kezeléssel" },
                    new Event { Id = 2, CourseId = 2, Name = "Genetikus algoritmusok, 1. dolgozat", Description = "A dolgozat megírásához szüksége lesz a MATLAB Global Optimization Toolbox-ára" },
                    new Event { Id = 3, CourseId = 3, Name = "3. Labor: Tesztelési jegyzőkönyv feltöltése", Description = "Software Fault Injection tesztelés" },
                    new Event { Id = 4, CourseId = 4, Name = "Év végi zárthelyi dolgozat feltöltése", Description = "Beszámoló a világegyetem megismerése során szerzett ismeretekről" },
                    new Event { Id = 5, CourseId = 5, Name = "Elméleti zárthelyi", Description = "A 143 kérdésből 10 sorsolt kérdésből álló teszt" },
                    new Event { Id = 6, CourseId = 6, Name = "2. zárthelyi dolgozat", Description = "Összetett konzol alkalmazás készítése" },
                    new Event { Id = 7, CourseId = 7, Name = "Év végi zárthelyi teszt", Description = "A haza védelme" },
                    new Event { Id = 8, CourseId = 8, Name = "1. zárthelyi dolgozat", Description = "Valószínűség számítás számonkérés" },
                    new Event { Id = 9, CourseId = 9, Name = "4. kZh", Description = "MongoDB kisZh" },
                    new Event { Id = 10, CourseId = 10, Name = "1. zh eredmények", Description = "Horváth Ádám csoportja (Cs 14-16)" },
                    new Event { Id = 11, CourseId = 11, Name = "Amit a héten át kell nézni", Description = "Utasítások, címzési módok, RISC/CISC processzorok" }
                };
                context.Events.AddRange(events);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
