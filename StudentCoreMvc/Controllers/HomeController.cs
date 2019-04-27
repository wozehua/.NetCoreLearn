using Microsoft.AspNetCore.Mvc;
using StudentCoreMvc.Model;
using StudentCoreMvc.Services;

namespace StudentCoreMvc.Controllers
{
    public class HomeController:Controller
    {
        private readonly IRepository<Student> _repository;

        public HomeController(IRepository<Student> repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var list = _repository.GetAll();

            return View(list);
        }

        public IActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            _repository.Add(student);
            //RedirectToAction 防止重复提交
            return RedirectToAction(nameof(Index));
        }
    }
}
