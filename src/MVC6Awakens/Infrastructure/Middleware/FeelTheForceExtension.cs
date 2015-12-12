using Microsoft.AspNet.Builder;

namespace MVC6Awakens.Infrastructure.Middleware
{
    public static class FeelTheForceExtension
    {
        public static IApplicationBuilder UseFeelTheForce(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FeelTheForceMiddleware>();
        }
    }
}
