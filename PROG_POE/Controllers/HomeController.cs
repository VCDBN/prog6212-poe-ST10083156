using Microsoft.AspNetCore.Mvc;
using PROG_POE.Models;
using System.Diagnostics;

namespace PROG_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //constructor for HomeController
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //GET action for Index page of Home controller
        public IActionResult Index()
        {
            return View();
        }

        //GET action for Privacdy page of Home controller

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
