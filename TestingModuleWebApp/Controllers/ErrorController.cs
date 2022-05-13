using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestingModuleWebApp.Models;

namespace TestingModuleWebApp.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                switch (statusCode)
                {
                    case 404:
                        return View("Error", new ErrorViewModel { RequestId = statusCode.ToString() });

                    default:
                        return View("Error");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
