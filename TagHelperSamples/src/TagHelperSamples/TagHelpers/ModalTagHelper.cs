using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.TagHelpers
{
    public class ModalTagHelper : TagHelper
    {
        /// <summary>
        /// The title of the modal
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Id of the modal
        /// </summary>
        public string Id { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await context.GetChildContentAsync();
            var content = childContent.GetContent();
            var template =
$@"<div class='modal-dialog' role='document'>
    <div class='modal-content'>
      <div class='modal-header'>
        <button type = 'button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
        <h4 class='modal-title' id='{Id}Label'>{Title}</h4>
      </div>
        {content}
    </div>
  </div>";
            output.TagName = "div";
            output.Attributes["role"] = "dialog";
            output.Attributes["id"] = Id;
            output.Attributes["aria-labelledby"] = $"{Id}Label";
            output.Attributes["tabindex"] = "-1";
            var classNames = "modal fade";
            if (output.Attributes.ContainsName("class"))
            {
                classNames = string.Format("{0} {1}", output.Attributes["class"].Value, classNames);
            }
            output.Attributes["class"] = classNames;

            output.Content.SetContent(template);
        }
    }
}
