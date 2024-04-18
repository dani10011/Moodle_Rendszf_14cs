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
        public int User_Id { get; set; }
        public int Course_Id { get; set; }
    }
}
