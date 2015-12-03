
using System.Collections.Generic;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ViewFeatures;


namespace MVC6Awakens.Controllers
{
    public class POCOController
    {
        private readonly IModelMetadataProvider provider;

        [ActionContext]
        public ActionContext ActionContext { get; set; }


        public POCOController(IModelMetadataProvider provider)
        {
            this.provider = provider;
        }


        // GET: /<controller>/
        public IActionResult Content()
        {
            return new ContentResult() { Content = "Hello from POCO controller!" };
        }
        public IActionResult Index()
        {
            var viewData = new ViewDataDictionary<string>(provider, ActionContext.ModelState);
            viewData.Model = "Hello from POCO controller!";
            return new ViewResult() { ViewData = viewData };
        }
    }
}