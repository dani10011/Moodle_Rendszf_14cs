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
                    new User { Id = 1, UserName = "H8UOA6", Name = "Kovács L. Bendegúz", Password = "1234", Degree_Id = 1},
                    new User { Id = 2, UserName = "B7FY4E", Name = "Virágh Áron", Password = "12345", Degree_Id = 1},
                    new User { Id = 3, UserName = "CT3QB5", Name = "Ökrös Dániel", Password = "Jelszo1", Degree_Id = 1},
                    new User { Id = 4, UserName = "URK2SP", Name = "Kovács Bence", Password = "ucw803", Degree_Id = 6},
                    new User { Id = 5, UserName = "A9T32P", Name = "Bálint Miklós", Password = "nagyafaszom", Degree_Id = 3},
                    new User { Id = 6, UserName = "FAROK5", Name = "Péter Árpád", Password = "titkos01", Degree_Id = 7},
                    new User { Id = 7, UserName = "XG739D", Name = "Minta Béla", Password = "mintajel", Degree_Id = 8},
                    new User { Id = 8, UserName = "OQNZ3B", Name = "Juhász Zoltán", Password = "java8", Degree_Id = 4},
                    new User { Id = 9, UserName = "2F84HR", Name = "Frits Márton", Password = "kreatin", Degree_Id = 2},
                    new User { Id = 10, UserName = "VQB9U4", Name = "Tarczali Tünde", Password = "ViAron04", Degree_Id = 5},
                    new User { Id = 11, UserName = "AR4K1S", Name = "Paul Atreides", Password = "LisanAlGaib", Degree_Id = 9}

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

            if (!context.Approved_Degrees.Any())
            {
                List<ApprovedDegree> approvedDegrees = new List<ApprovedDegree>
                {
                    new ApprovedDegree { Id = 1, Course_Id = 1, Degree_Id = 1 },
                    new ApprovedDegree { Id = 2, Course_Id = 1, Degree_Id = 2 },
                    new ApprovedDegree { Id = 3, Course_Id = 1, Degree_Id = 3 },
                    new ApprovedDegree { Id = 4, Course_Id = 1, Degree_Id = 4 },

                    new ApprovedDegree { Id = 5, Course_Id = 2, Degree_Id = 1 },
                    new ApprovedDegree { Id = 6, Course_Id = 2, Degree_Id = 2 },
                    new ApprovedDegree { Id = 7, Course_Id = 2, Degree_Id = 3 },
                    new ApprovedDegree { Id = 8, Course_Id = 2, Degree_Id = 4 },

                    new ApprovedDegree { Id = 9, Course_Id = 3, Degree_Id = 1 },
                    new ApprovedDegree { Id = 10, Course_Id = 3, Degree_Id = 2 },
                    new ApprovedDegree { Id = 11, Course_Id = 3, Degree_Id = 3 },
                    new ApprovedDegree { Id = 12, Course_Id = 3, Degree_Id = 4 },
                        
                    new ApprovedDegree { Id = 13, Course_Id = 4, Degree_Id = 1 },
                    new ApprovedDegree { Id = 14, Course_Id = 4, Degree_Id = 2 },
                    new ApprovedDegree { Id = 15, Course_Id = 4, Degree_Id = 3 },
                    new ApprovedDegree { Id = 16, Course_Id = 4, Degree_Id = 4 },
                    new ApprovedDegree { Id = 17, Course_Id = 4, Degree_Id = 5 },
                    new ApprovedDegree { Id = 18, Course_Id = 4, Degree_Id = 6 },
                    new ApprovedDegree { Id = 19, Course_Id = 4, Degree_Id = 7 },
                    new ApprovedDegree { Id = 20, Course_Id = 4, Degree_Id = 8 },
                    new ApprovedDegree { Id = 21, Course_Id = 4, Degree_Id = 9 },
                    new ApprovedDegree { Id = 22, Course_Id = 4, Degree_Id = 10 },
                    new ApprovedDegree { Id = 23, Course_Id = 4, Degree_Id = 11 },

                    new ApprovedDegree { Id = 24, Course_Id = 5, Degree_Id = 1 },
                    new ApprovedDegree { Id = 25, Course_Id = 5, Degree_Id = 2 },
                    new ApprovedDegree { Id = 26, Course_Id = 5, Degree_Id = 3 },
                    new ApprovedDegree { Id = 27, Course_Id = 5, Degree_Id = 4 },

                    new ApprovedDegree { Id = 28, Course_Id = 6, Degree_Id = 1 },
                    new ApprovedDegree { Id = 29, Course_Id = 6, Degree_Id = 2 },
                    new ApprovedDegree { Id = 30, Course_Id = 6, Degree_Id = 3 },
                    new ApprovedDegree { Id = 31, Course_Id = 6, Degree_Id = 4 },

                    new ApprovedDegree { Id = 32, Course_Id = 7, Degree_Id = 1 },
                    new ApprovedDegree { Id = 33, Course_Id = 7, Degree_Id = 2 },
                    new ApprovedDegree { Id = 34, Course_Id = 7, Degree_Id = 3 },
                    new ApprovedDegree { Id = 35, Course_Id = 7, Degree_Id = 4 },
                    new ApprovedDegree { Id = 36, Course_Id = 7, Degree_Id = 5 },
                    new ApprovedDegree { Id = 37, Course_Id = 7, Degree_Id = 6 },
                    new ApprovedDegree { Id = 38, Course_Id = 7, Degree_Id = 7 },
                    new ApprovedDegree { Id = 39, Course_Id = 7, Degree_Id = 8 },
                    new ApprovedDegree { Id = 40, Course_Id = 7, Degree_Id = 9 },
                    new ApprovedDegree { Id = 41, Course_Id = 7, Degree_Id = 10 },

                    new ApprovedDegree { Id = 42, Course_Id = 8, Degree_Id = 1 },
                    new ApprovedDegree { Id = 43, Course_Id = 8, Degree_Id = 2 },
                    new ApprovedDegree { Id = 44, Course_Id = 8, Degree_Id = 3 },
                    new ApprovedDegree { Id = 45, Course_Id = 8, Degree_Id = 4 },

                    new ApprovedDegree { Id = 46, Course_Id = 9, Degree_Id = 1 },
                    new ApprovedDegree { Id = 47, Course_Id = 9, Degree_Id = 2 },
                    new ApprovedDegree { Id = 48, Course_Id = 9, Degree_Id = 3 },
                    new ApprovedDegree { Id = 49, Course_Id = 9, Degree_Id = 4 },

                    new ApprovedDegree { Id = 50, Course_Id = 10, Degree_Id = 1 },
                    new ApprovedDegree { Id = 51, Course_Id = 10, Degree_Id = 2 },
                    new ApprovedDegree { Id = 52, Course_Id = 10, Degree_Id = 3 },
                    new ApprovedDegree { Id = 53, Course_Id = 10, Degree_Id = 4 },
                    new ApprovedDegree { Id = 54, Course_Id = 10, Degree_Id = 7 }
                };
                context.Approved_Degrees.AddRange(approvedDegrees);
                await context.SaveChangesAsync();
            }

            if (!context.MyCourses.Any())
            {
                List<MyCourse> myCourses = new List<MyCourse>
                {
                    new MyCourse { Id = 1, User_Id = 1, Course_Id = 8 },
                    new MyCourse { Id = 2, User_Id = 1, Course_Id = 11 },
                    new MyCourse { Id = 3, User_Id = 2, Course_Id = 5 },
                    new MyCourse { Id = 4, User_Id = 2, Course_Id = 6 },
                    new MyCourse { Id = 5, User_Id = 3, Course_Id = 3 },
                    new MyCourse { Id = 6, User_Id = 3, Course_Id = 2 },
                    new MyCourse { Id = 7, User_Id = 4, Course_Id = 4 },
                    new MyCourse { Id = 8, User_Id = 4, Course_Id = 7 },
                    new MyCourse { Id = 9, User_Id = 5, Course_Id = 9 },
                    new MyCourse { Id = 10, User_Id = 5, Course_Id = 11 },
                    new MyCourse { Id = 11, User_Id = 6, Course_Id = 4 },
                    new MyCourse { Id = 12, User_Id = 6, Course_Id = 7 },
                    new MyCourse { Id = 13, User_Id = 7, Course_Id = 4 },
                    new MyCourse { Id = 14, User_Id = 7, Course_Id = 7 },
                    new MyCourse { Id = 15, User_Id = 8, Course_Id = 6 },
                    new MyCourse { Id = 16, User_Id = 8, Course_Id = 9 },
                    new MyCourse { Id = 17, User_Id = 9, Course_Id = 1 },
                    new MyCourse { Id = 18, User_Id = 9, Course_Id = 9 },
                    new MyCourse { Id = 19, User_Id = 10, Course_Id = 4 },
                    new MyCourse { Id = 20, User_Id = 10, Course_Id = 7 },
                    new MyCourse { Id = 21, User_Id = 11, Course_Id = 4 },
                    new MyCourse { Id = 22, User_Id = 11, Course_Id = 7 },
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
                    new Event { Id = 11, Course_Id = 11, Name = "Amit a héten át kell nézni", Description = "Utasítások, címzési módok, RISC/CISC processzorok" }
                };
                context.Events.AddRange(events);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
