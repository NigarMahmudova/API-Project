using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Core.Entities
{
    public class Group:BaseEntity
    {
        public string No { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
