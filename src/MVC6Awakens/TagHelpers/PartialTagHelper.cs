using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewEngines;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;

namespace MVC6Awakens.TagHelpers
{
    [HtmlTargetElement("partial", Attributes = "name")]
    public class PartialTagHelper : TagHelper
    {
        private readonly ICompositeViewEngine viewEngine;

        public PartialTagHelper(ICompositeViewEngine viewEngine)
        {
            this.viewEngine = viewEngine;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string Name { get; set; }

        public object Model { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            var partialResult = viewEngine.FindPartialView(ViewContext, Name);

            if (partialResult != null && partialResult.Success)
            {
                var partialViewData = new ViewDataDictionary(ViewContext.ViewData, Model);
                var partialWriter = new TagHelperContentWrapperTextWriter(ViewContext.Writer.Encoding, output.Content);
                var partialViewContext = new ViewContext(ViewContext, partialResult.View, partialViewData, partialWriter);

                await partialResult.View.RenderAsync(partialViewContext);
            }
        }
    }
}
