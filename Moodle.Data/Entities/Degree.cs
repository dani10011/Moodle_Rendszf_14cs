using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodle.Data.Entities
{
    public class Degree
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation
        public ICollection<User> Users { get; set; }
        public ICollection<ApprovedDegree> ApprovedDegrees { get; set; }
    }
}
