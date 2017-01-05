using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    /// <summary>
    /// Panel Footer Tag Helper
    /// </summary>
    [HtmlTargetElement("panel-footer", ParentTag = "panel")]
    public class PanelFooterTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var modalContext = (PanelContext)context.Items[typeof(PanelTagHelper)];
            modalContext.Footer = childContent;
            output.SuppressOutput();
        }
    }
}
