using Microsoft.AspNet.Mvc;
using System.Reflection;

namespace MVC6Awakens.Controllers
{
    public class FrameworkController : Controller
    {
        public string Index()
        {
            var assembly = "";
            #if DNX451
                // utilize resource only available with .NET Framework
                assembly = Assembly.GetExecutingAssembly().FullName;
            #endif
            #if DNXCORE50
                 // utilize resource only available with .NET Framework
                assembly=typeof(FrameworkController).GetTypeInfo().Assembly.FullName;
            #endif
            return assembly;
        }
    }
}
