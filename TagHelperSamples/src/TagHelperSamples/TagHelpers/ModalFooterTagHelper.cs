using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.TagHelpers
{
    public class ModalFooterTagHelper : TagHelper
    {
        public bool ShowDismiss { get; set; } = true;

        public string DismissText { get; set; } = "Cancel";


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await context.GetChildContentAsync();

            if (ShowDismiss)
            {
                var dismissTemplate = $@"<button type='button' class='btn btn-default' data-dismiss='modal'>{DismissText}</button>";
                output.PreContent.SetContent(dismissTemplate);
            }
            output.TagName = "div";
            var classNames = "modal-footer";
            if (output.Attributes.ContainsName("class"))
            {
                classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
            }
            output.Attributes["class"] = classNames;
            output.Content.SetContent(childContent);
        }
    }
}
