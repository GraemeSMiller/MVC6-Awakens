using Microsoft.AspNet.Razor.TagHelpers;

namespace MVC6Awakens.TagHelpers
{
    /// <summary>
    /// This is a very simple helper that can be applied to any HTML element. If you add Visible="<A Boolean Value or Express>" then it will hide the element if the boolean is false
    /// Easy way to make elements be conditionally visible.
    /// </summary>
    [HtmlTargetElement(Attributes = nameof(Visble))]
    public class VisbleTagHelper : TagHelper
    {
        public bool Visble { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Visble)
            {
                output.SuppressOutput();
            }
        }
    }
}