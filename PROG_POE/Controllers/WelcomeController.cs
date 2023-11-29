using Microsoft.AspNetCore.Mvc;
using PROG_POE.Models;

namespace PROG_POE.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly StudentStudyContext _context;

        public WelcomeController(StudentStudyContext context)
        {
            _context = context;
        }

        //Get action for welcome page
        //Checks whether user is logged in or not and displays messages based on that
        public IActionResult Welcome() 
        {
            ViewBag.Login = HttpContext.Session.GetString("UserLoggedIn");
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
