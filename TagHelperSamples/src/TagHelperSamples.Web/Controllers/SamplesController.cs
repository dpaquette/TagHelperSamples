using System;
using Microsoft.AspNetCore.Mvc;
using TagHelperSamples.Web.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using GenFu;

namespace TagHelperSamples.Web.Controllers
{
    public class SamplesController : Controller
    {

        public IActionResult Authorize()
        {
            var model = new LoginModel();
            if (User.Identity.IsAuthenticated)
            {
                model.UserName = User.Identity.Name;
                model.Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                model.Age = User.Claims.FirstOrDefault(c => c.Type == "Age")?.Value;
            }
            A.Configure<Document>().Fill(d => d.Author).WithRandom(new[] {"Joe", "Jane", "Jim", "James"});
            ViewBag.Documents = A.ListOf<Document>(15);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var claimsIdentity = new ClaimsIdentity("TestAuthenticationType");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, model.Role));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));
            claimsIdentity.AddClaim(new Claim("Age", model.Age.ToString()));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Authorize");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Authorize");
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
