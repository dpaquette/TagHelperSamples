using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.TagHelpers
{
    public class ModalBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await context.GetChildContentAsync();
            
          
            output.TagName = "div";
            var classNames = "modal-body";
            if (output.Attributes.ContainsName("class"))
            {
                classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
            }
            output.Attributes["class"] = classNames;
            output.Content.SetContent(childContent);
        }
    }
}
