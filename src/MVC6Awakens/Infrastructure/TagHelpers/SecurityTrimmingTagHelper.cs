using System.Threading.Tasks;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;

using MVC6Awakens.Infrastructure.Security;


namespace MVC6Awakens.Infrastructure.TagHelpers
{
    /// <summary>
    /// This is a very simple helper that can be applied to any HTML element. If you add Visible="<A Boolean Value or Express>" then it will hide the element if the boolean is false
    /// Easy way to make elements be conditionally visible.
    /// </summary>
    [HtmlTargetElement(Attributes = nameof(Policy))]
    public class SecurityTrimmingTagHelper : TagHelper
    {
        public override int Order => -1;

        private readonly IAuthorizationService authorizationService;

        public string Policy { get; set; }


        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public SecurityTrimmingTagHelper(IAuthorizationService authorizationService, IHttpContextAccessor HttpContextAccessor)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this.authorizationService = authorizationService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Policy))
            {
                var canSee = await authorizationService.AuthorizeAsync(HttpContextAccessor.HttpContext.User, Policy);
                if (!canSee)
                {
                    output.SuppressOutput();
                }
            }

        }
    }
}