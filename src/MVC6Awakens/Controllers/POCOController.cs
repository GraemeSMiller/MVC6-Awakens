
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;


namespace MVC6Awakens.Controllers
{
    public class POCOController
    {
        [ActionContext]
        public ActionContext ActionContext { get; set; }




        // GET: /<controller>/
        public IActionResult Content()
        {
            return new ContentResult() { Content = "Hello from POCO controller!" };
        }
        public IActionResult Index()
        {
            return new ViewResult() { };
        }
    }
}