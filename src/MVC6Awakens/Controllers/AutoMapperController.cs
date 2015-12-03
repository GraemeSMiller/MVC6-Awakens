using Microsoft.AspNet.Mvc;

using MVC6Awakens.Models;
using MVC6Awakens.ViewModels.AutoMapper;


namespace MVC6Awakens.Controllers
{
    public class AutoMapperController : Controller
    {
        public IActionResult Index()
        {
            var domain = new DomainModelTest() { Name = "Test" };
            var test = AutoMapper.Mapper.Map<DomainModelTestViewModel>(domain);
            return View(test);
        }
    }
}
