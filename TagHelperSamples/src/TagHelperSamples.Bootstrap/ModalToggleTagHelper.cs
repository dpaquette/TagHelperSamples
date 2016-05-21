using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelperSamples.Bootstrap
{
    /// <summary>
    /// Sets the element as the item that will toggle the specified Bootstrap modal dialog
    /// </summary>
    [HtmlTargetElement(Attributes = ModalTargetAttributeName)]
    public class ModalToggleTagHelper : TagHelper
    {
        public const string ModalTargetAttributeName = "bs-toggle-modal";
        
        /// <summary>
        /// The id of the modal that will be toggled by this element
        /// </summary>
        [HtmlAttributeName(ModalTargetAttributeName)]
        public string ToggleModal { get; set; }

      
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("data-toggle", "modal");
            output.Attributes.SetAttribute("data-target", $"#{ToggleModal}");
        }
    }
}
