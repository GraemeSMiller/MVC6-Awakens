using Microsoft.AspNet.Mvc.Filters;


namespace MVC6Awakens.Infrastructure
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //this.logger.Log(actionContext.HttpContext.Request);
            var somethingICouldLog = actionContext.HttpContext.Request;
        }
    }
}
