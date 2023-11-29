using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG_POE.Models;
using PROG_POE.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROG_POE.Controllers
{
    public class StudyHoursController : Controller
    {
        //variable to get db context for access to semester table
        private readonly StudentStudyContext _context;

        //selectList to help populate dropdown list in Views
        public SelectList moduleOptions { get; set; }

        //Constructor instantialising dbContext
        public StudyHoursController(StudentStudyContext context)
        {
            _context = context;
        }

        //Get action for study hours Index page
        public async Task<IActionResult> Index()
        {
            //verifies that user is loggen in before displaying content
            if (HttpContext.Session.GetString("UserLoggedIn") != null)
            {

                var userStudyHours = _context.Semesters.Where(s => s.Username == HttpContext.Session.GetString("UserLoggedIn")).ToList();
                return View(userStudyHours.ToList());
            }
            //displays error message and redirects to login page if user is not logged in
            else
            {
                TempData["LoginRequired"] = "Login required for access to study hours!";
                return RedirectToAction("Login", "Login");
            }
           
        }

        //Get action for update view, displays options to update study hours and populates selectlist with module codes
        [HttpGet]
        public IActionResult UpdateHours() 
        {
          
            string username = HttpContext.Session.GetString("UserLoggedIn");
            var modules = _context.GetModules(username);
            var model = new StudyHoursViewModel();
            model.ModulesSelectList= new List<SelectListItem>();

            foreach (var module in modules) 
            {
                model.ModulesSelectList.Add(new SelectListItem { Text = module.Name, Value = module.Code });
            }
            ViewBag.ModuleOptions = model.ModulesSelectList;
            return View(model);
        }

        //Verifies that user input is valid and updates study hours
        //if user input is invalid, redirects back to get action
        [HttpPost]
        public IActionResult UpdateHours(StudyHoursViewModel model, int WeekNum, int HrsStudied)
        {
            if (WeekNum > Semester.NumWeeks || WeekNum<0) 
            {
                ViewBag.Error = "The week number is invalid!";
                return RedirectToAction(nameof(UpdateHours));
            }
           
            string username = HttpContext.Session.GetString("UserLoggedIn");
            foreach (Semester semester in _context.Semesters.Where(s=>s.Code == model.SelectedModule && WeekNum == s.WeekNum && s.Username == username))
            {
                if (HrsStudied > semester.StudyHours || HrsStudied < 0)
                {
                    ViewBag.Error = "The amount of hours you have entered is invalid";
                    return RedirectToAction(nameof(UpdateHours));
                }
                semester.StudyHours = semester.StudyHours - HrsStudied;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
