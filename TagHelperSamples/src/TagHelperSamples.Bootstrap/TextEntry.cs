using System.Text;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

/*
This TH was written by Rick Strahl 

Usage:

<text-entry id="Name"  value="@Model.Name" label="Name:" placeholder="Enter your name.">
</text-entry

*/

namespace TagHelperSamples.Bootstrap
{

    /// <summary>
    /// Simple tag helper that creates a label and textbox combination in
    /// easier to read HTML than the full bootstrap form-group format.
    /// </summary>
    [HtmlTargetElement("text-entry")]
    public class TextEntryTagHelper : TagHelper
    {
        /// <summary>
        /// the main message that gets displayed
        /// </summary>
        [HtmlAttributeName("label-text")]
        public string LabelText { get; set; }

        /// <summary>
        /// Optional header that is displayed in big text. Use for 
        /// 'noisy' warnings and stop errors only please :-)
        /// The message is displayed below the header.
        /// </summary>
        [HtmlAttributeName("value")]
        public string Value { get; set; }

        [HtmlAttributeName("placeholder")]
        public string PlaceHolder { get; set; }
        
        /// <summary>
        /// CSS class. Handled here so we can capture the existing
        /// class value and append the BootStrap alert class.
        /// </summary>
        [HtmlAttributeName("textbox-class")]
        public string TextBoxClass { get; set; } = "form-control";

        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("type")]
        public string Type { get; set; } = "text";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder sb = new StringBuilder();

            string type = Type;
            string labelText = LabelText;
            string value = Value;
            string textClass = TextBoxClass;
            string placeholder = PlaceHolder;

            if (string.IsNullOrEmpty(value))
            {
                if (For != null)
                {
                    value = For.Model.ToString();
                }
            }

            output.TagName = "div";
            output.Attributes.Add("class", "form-group");

            var ctl = new TagBuilder("label");
            ctl.Attributes.Add("for", Id);
            ctl.InnerHtml.Append(labelText);
            sb.AppendLine(ctl.ToString());

            ctl = new TagBuilder("input");
            ctl.Attributes.Add("id", Id);
            ctl.Attributes.Add("name", Id);
            ctl.AddCssClass(textClass);
            ctl.Attributes.Add("value", value);
            if (!string.IsNullOrEmpty(placeholder))
                ctl.Attributes.Add("placeholder", placeholder);

            sb.AppendLine( ctl.ToString() );

            // TODO: Add ValidationMessage           
            //ctl = new TagBuilder("span");
            

            
            output.Content.SetContent(sb.ToString());
        }
    }
}