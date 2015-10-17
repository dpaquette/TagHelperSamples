using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TagHelperSamples.Web.Model;

namespace TagHelperSamples.Web.Controllers
{
   public class HomeController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }

      public IActionResult About()
      {      
         Random random = new Random();
         int randomNumber = random.Next(0, 100);

         return View(new TestModel { CurrentProgress = randomNumber,
                          Message = "An error occurred while trying to save customer information." });
      }

      public IActionResult Contact()
      {
         ViewData["Message"] = "Your contact page.";

         return View();
      }

      public IActionResult Error()
      {
         return View("~/Views/Shared/Error.cshtml");
      }
   }
}
