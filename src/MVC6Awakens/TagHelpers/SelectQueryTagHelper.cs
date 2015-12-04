using Microsoft.AspNet.Razor.TagHelpers;

using MVC6Awakens.Infrastructure.Dropdowns;


namespace MVC6Awakens.TagHelpers
{
    [HtmlTargetElement("select", Attributes = "query-for")]
    public class SelectQueryTagHelper: TagHelper
    {
        public bool Wrap { get; set; }
        public IDropdownQuery QueryFor { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var x = QueryFor;
        }
    }
}