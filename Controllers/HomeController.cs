using matxicorp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace webgalleriet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        [Route("/Home/HandleErrorWithStatusCode/{code}")]
        public IActionResult HandleErrorWithStatusCode(string code)
        {
            string errorMessage = string.Empty;
            if (code == "401")
            {
                errorMessage = "You do not have permission to access this page.";
            }
            else if (code == "404")
            {
                errorMessage = "Page Not Found";
            }

            ViewData["ErrorMessage"] = errorMessage;
            return View();
        }
    }
}
