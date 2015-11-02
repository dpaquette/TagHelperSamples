using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Markdown
{
    [HtmlTargetElement("p", Attributes = "markdown")]
    [HtmlTargetElement("markdown")]
    [OutputElementHint("p")]
    public class MarkdownTagHelper : TagHelper
    {
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (output.TagName == "markdown")
            {
                output.TagName = "p";
            }
            var childContent = await context.GetChildContentAsync();
            var markdownContent = childContent.GetContent();

            var markdown = new MarkdownSharp.Markdown();
            var htmlContent = markdown.Transform(markdownContent);
            output.Content.SetContentEncoded(htmlContent);
        }
    }
}
