using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;


namespace MVC6Awakens.Infrastructure.TagHelpers
{

    /// <summary>
    /// Allows consistent generation of standard bootstrap form groups.
    /// Each group consists of a form group div, a label, a control div, an input or select element, a required indidicator
    /// </summary>
    [HtmlTargetElement("input", Attributes = nameof(Wrap) + ", asp-for")]
    [HtmlTargetElement("select", Attributes = nameof(Wrap) + ", asp-for")]
    public class FormGroupWrapperTagHelper : TagHelper
    {
        //Obtain an HTML genertor so we can use it to generate labels, validation
        public FormGroupWrapperTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <inheritdoc />
        public override int Order
        {
            get
            {
                return -1000;
            }
        }

        //Inject the current pages viewcontext so we can use it with helpers
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }
        public bool Wrap { get; set; }
        public ModelExpression AspFor { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            //Obtain the target model property
            var modelExpressionName = AspFor.Name;
            //Check meta data to see if propert is required
            var required = AspFor.Metadata.IsRequired;
            //Check to see if there is a displayname and store for use as label
            var displayName = AspFor.Metadata.DisplayName ?? modelExpressionName;

            //Creating elements that will appear before the targeted element
            output.PreElement.SetHtmlContent("<div class=\"form-group\">");

            //Create label using generator
            var generateLabel = Generator.GenerateLabel(ViewContext, AspFor.ModelExplorer, AspFor.Name, displayName, null);
            generateLabel.AddCssClass("col-md-2");
            generateLabel.AddCssClass("control-label");
            output.PreElement.Append(generateLabel);
            output.PreElement.AppendHtml("<div class=\"col-md-10\">");


            // We are now moving past the targeted element. All below here comes after the element

            if (required)
            {
                //Create a span the will visilby show the required indicator
                var requiredSpan = new TagBuilder("span");
                requiredSpan.AddCssClass("required");
                requiredSpan.AddCssClass("indicator");
                requiredSpan.InnerHtml.Append("*");
                output.PostElement.Append(requiredSpan);
            }
            //Use the generator to create a standard validation
            var generateValidationMessage = Generator.GenerateValidationMessage(ViewContext,
                                                         AspFor.Name,
                                                         message: null,
                                                         tag: null,
                                                         htmlAttributes: null);
            output.PostElement.Append(generateValidationMessage);

            //Close the div tags - I think this code can be cleaned up and done more automatically but it works
            output.PostElement.AppendHtml("</div>");
            output.PostElement.AppendHtml("</div>");
        }
    }
}