using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCoreMvc.Model;

namespace StudentCoreMvc.Services
{
    public class InMemoryRepository:IRepository<Student>
    {
        private readonly List<Student> _student;

        public InMemoryRepository()
        {
            _student = new List<Student>
            {
                new Student()
                {
                    Id = 1,
                    FirstName = "cai",
                    SecondName = "zehua"
                },
                new Student()
                {
                    Id =2,
                    FirstName = "asdf",
                    SecondName = "asdf"
                },
                new Student()
                {
                    Id = 3,
                    FirstName = "erte",
                    SecondName = "gsdfg"
                },
            };
        }
        public IEnumerable<Student> GetAll()
        {
            return _student;
        }

        public void Add(Student student)
        {
            var currentId = _student.Max(x => x.Id);
            student.Id = currentId + 1;
            _student.Add(student);
        }
    }
}
