using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentCoreMvc.Model;
using StudentCoreMvc.Services;

namespace StudentCoreMvc.ViewComponents
{
    public class WelcomeViewComponent:ViewComponent
    {
        private readonly IRepository<Student> _repository;

        public WelcomeViewComponent(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var count = _repository.GetAll().Count();
            return View("StudentNumber", count);
        }
    }

}
