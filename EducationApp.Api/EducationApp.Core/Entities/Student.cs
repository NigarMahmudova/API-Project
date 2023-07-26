using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Core.Entities
{
    public class Student:BaseEntity
    {
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public decimal Point { get; set; }

        public Group Group { get; set; }
    }
}
