using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    /// <summary>
    /// Panel Title Tag Helper
    /// </summary>
    [HtmlTargetElement("panel-title", ParentTag = "panel")]
    public class PanelTitleTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            var modalContext = (PanelContext)context.Items[typeof(PanelTagHelper)];
            modalContext.Title = childContent;
            output.SuppressOutput();
        }
    }
}
