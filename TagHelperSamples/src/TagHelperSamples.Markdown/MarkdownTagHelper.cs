using System.Threading.Tasks;
using CommonMark;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace TagHelperSamples.Markdown
{
    [HtmlTargetElement("markdown", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement(Attributes = "markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        public ModelExpression Content { get; set; }
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (output.TagName == "markdown")
            {
                output.TagName = null;
            }
            output.Attributes.RemoveAll("markdown");

            var content = await GetContent(context);
            var markdown = content;
            var html = CommonMarkConverter.Convert(markdown);
            output.Content.SetContentEncoded(html ?? "");
        }

        private async Task<string> GetContent(TagHelperContext context)
        {
            if (Content == null)
                return (await context.GetChildContentAsync()).GetContent();

            return Content.Model?.ToString();
        }
    }
}
