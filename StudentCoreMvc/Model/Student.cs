using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCoreMvc.Model
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime DateTime { get; set; }

        public Gender Sex { get; set; }
    }
}
