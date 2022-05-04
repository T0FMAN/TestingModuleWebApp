using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(AnswersTask answersTask)
        {
            return View(answersTask);
        }

        [HttpPost]
        public IActionResult MyPost([FromForm] int x, [FromForm] int y)
        {
            

            //SaveResult(x, y);

            return Ok();
        }

        [HttpPost]
        public IActionResult Result(AnswersTask answersTask, int x)
        {
            if (!ModelState.IsValid)
            {
                return View("False");
            }
            return View("True");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}