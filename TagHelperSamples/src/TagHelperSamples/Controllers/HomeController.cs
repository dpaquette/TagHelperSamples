using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TagHelperSamples.Model;

namespace TagHelperSamples.Controllers
{
   public class HomeController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }

      public IActionResult About()
      {
         ViewData["Message"] = "Your application description page.";
         Random random = new Random();
         int randomNumber = random.Next(0, 100);
         return View(new TestModel { CurrentProgress = randomNumber, Message = "Test" });
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
