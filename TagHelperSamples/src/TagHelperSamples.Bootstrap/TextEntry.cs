using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
            if (string.IsNullOrEmpty(Value))
            {
                if (For != null)
                {
                    Value = For.Model.ToString();
                }
            }

            output.TagName = "div";
            output.Attributes.Add("class", "form-group");

            var writer = new System.IO.StringWriter();
            CreateLabelTagBuilder().WriteTo(writer, HtmlEncoder.Default);
            CreateInputTagBuilder().WriteTo(writer, HtmlEncoder.Default);
            // TODO: Add ValidationMessage

            output.Content.SetHtmlContent(writer.ToString());
        }

        private TagBuilder CreateLabelTagBuilder()
        {
            var labelBuilder = new TagBuilder("label");

            labelBuilder.Attributes.Add("for", Id);
            labelBuilder.InnerHtml.Append(LabelText);

            return labelBuilder;
        }

        private TagBuilder CreateInputTagBuilder()
        {
            var inputBuilder = new TagBuilder("input");

            inputBuilder.Attributes.Add("id", Id);
            inputBuilder.Attributes.Add("name", Id);
            inputBuilder.AddCssClass(TextBoxClass);
            inputBuilder.Attributes.Add("value", Value);
            if (!string.IsNullOrEmpty(PlaceHolder))
            {
                inputBuilder.Attributes.Add("placeholder", PlaceHolder);
            }

            return inputBuilder;
        }
    }
}

