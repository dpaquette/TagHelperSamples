using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Threading.Tasks;

namespace TagHelperSamples.TagHelpers
{
    [TargetElement(Attributes = ModalTargetAttributeName)]
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
            output.Attributes["data-toggle"] = "modal";
            output.Attributes["data-target"] = $"#{ToggleModal}";
        }
    }
}
