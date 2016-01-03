using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.TagHelpers;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperSamples.Bootstrap
{
    public class NavLinkTagHelper : AnchorTagHelper
    {
        public NavLinkTagHelper(IHtmlGenerator generator)
            : base(generator)
        {
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = output.GetChildContentAsync().Result.GetContent();
            base.Process(context, output);
            output.TagName = "li";
            var hrefAttr = output.Attributes.FirstOrDefault(a => a.Name == "href");
            if (hrefAttr != null)
            {
                output.Content.SetHtmlContent($@"<a href=""{hrefAttr.Value}"">{content}</a>");
                output.Attributes.Remove(hrefAttr);
            }
            else
                output.Content.SetHtmlContent(content);

            if (ShouldBeActive())
            {
                MakeActive(output);
            }
        }

        private bool ShouldBeActive()
        {
            string currentController = ViewContext.RouteData.Values["Controller"].ToString();
            string currentAction = ViewContext.RouteData.Values["Action"].ToString();
            bool res;
            if (!string.IsNullOrWhiteSpace(Controller) && !string.IsNullOrWhiteSpace(Action))
                res = Controller == currentController && Action == currentAction;
            else if (!string.IsNullOrWhiteSpace(Action))
                res = Action == currentAction;
            else
                res = false;
            return res;
        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf("active") < 0)
            {
                classAttr.Value = classAttr.Value == null
                    ? "value"
                    : classAttr.Value.ToString() + " active";
            }
        }

    }
}
