using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    /// <summary>
    /// Panel Tag Helper
    /// </summary>
    [RestrictChildren("panel-title", "panel-body", "panel-footer")]
    public class PanelTagHelper : TagHelper
    {
        public PanelType Type { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var panelContext = new PanelContext();
            context.Items.Add(typeof(PanelTagHelper), panelContext);

            await output.GetChildContentAsync();

            output.TagName = "div";

            // Default panel type will be panel-default
            output.Attributes.Add("class", $"panel panel-{Type.ToString().ToLower()}");

            // panel title
            if (panelContext.Title != null)
            {
                var h3 = new TagBuilder("h3");
                h3.AddCssClass("panel-title");
                h3.InnerHtml.AppendHtml(panelContext.Title);

                var panelTitle = new TagBuilder("div");
                panelTitle.AddCssClass("panel-heading");
                panelTitle.InnerHtml.AppendHtml(h3);

                output.Content.AppendHtml(panelTitle);
            }

            // panel body
            if (panelContext.Body != null)
            {
                var panelBody = new TagBuilder("div");
                panelBody.AddCssClass("panel-body");
                panelBody.InnerHtml.AppendHtml(panelContext.Body);

                output.Content.AppendHtml(panelBody);
            }

            // panel footer
            if (panelContext.Footer != null)
            {
                var panelFooter = new TagBuilder("div");
                panelFooter.AddCssClass("panel-footer");
                panelFooter.InnerHtml.AppendHtml(panelContext.Footer);

                output.Content.AppendHtml(panelFooter);
            }
        }
    }
}
