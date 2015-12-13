using Microsoft.AspNet.Mvc;

namespace MVC6Awakens.Controllers
{
    public class PartialController : Controller
    {
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}
