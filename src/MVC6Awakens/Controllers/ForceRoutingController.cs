using Microsoft.AspNet.Mvc;

namespace MVC6Awakens.Controllers
{
    [Route("The")]
    public class ForceRoutingController : Controller
    {
        [Route("Force/Awakens")]
        public string Index()
        {
            return "Test";
        }
    }
}