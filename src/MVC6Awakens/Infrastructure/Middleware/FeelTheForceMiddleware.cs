using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace MVC6Awakens.Infrastructure.Middleware
{
    public class FeelTheForceMiddleware
    {
        private readonly RequestDelegate next;
        public FeelTheForceMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Append("X-Application", "MVC6 Awakens - Feel The Force");
            await next.Invoke(context);
        }
    }
}
