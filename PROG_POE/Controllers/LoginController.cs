using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using PROG_POE.Models;

namespace PROG_POE.Controllers
{
    public class LoginController : Controller
    {
        //Retrieving Database context for acces to users
        private readonly StudentStudyContext _context;

        //constructor instantialising the context
        public LoginController(StudentStudyContext context)
        {
            _context = context;
        }

        //Get action for Login page in LoginController
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Post action which checks whether user is in the database and whether password matches stored password
        [HttpPost]
        public IActionResult Login(string Username, string Password) 
        {
        User u = _context.Users.Where(u=> u.Username == Username && u.Password == Password).FirstOrDefault();
            if (u != null)
            {
                //login function sets session variable
                HttpContext.Session.SetString("UserLoggedIn", u.Username);
                TempData["UserAsTempData"] = u.Name;
                TempData["UsernameAsTempData"] = u.Username;
                return RedirectToAction("Index", "Module");
            }
            else 
            {
                //Error message in case login function fails
                ViewBag.Error = "Incorrect Login Details!";
                return View();
            }
        }

        //logout action which clears session variable and empties static NumWeeks variable in Semester model
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Semester.NumWeeks=0;
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
