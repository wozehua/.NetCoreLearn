using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCoreMvc.Model;

namespace StudentCoreMvc.Services
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        void Add(T student);
    }
}
