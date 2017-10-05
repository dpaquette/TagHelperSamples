using System;
using Microsoft.AspNetCore.Mvc;
using TagHelperSamples.Web.Model;

namespace TagHelperSamples.Web.Controllers
{
    public class SamplesController : Controller
    {

        public IActionResult Authorization()
        {
            return View();
        }

        public IActionResult AlertTagHelper()
        {
                var model = new TestModel
                {
                    Header = "Unable to save form",
                    Message = "Please fix the highlighted errors on the form below."
                };
        
            return View(model);
        }

        public IActionResult ProgressBarTagHelper()
        {
            Random random = new Random();
            
            var model = new TestModel
            {
                CurrentProgress = random.Next(0, 100)
            };
            return View(model);
        }

        public IActionResult ModalTagHelper()
        {
            return View();
        }

        public IActionResult NavLinkTagHelper()
        {
            return View();
        }

        public IActionResult PanelTagHelper()
        {
            return View();
        }

        public IActionResult MarkdownTagHelper()
        {
            return View();
        }


        public IActionResult TextEntryTagHelper(TestModel model = null)
        {
            if (model == null || model.Header == null && model.Message == null)
            {

                model = new TestModel
                {
                    Header = "Unable to save form",
                    Message = "Please fix the highlighted errors on the form below."
                };
            }
            return View(model);
        }

    }
}
