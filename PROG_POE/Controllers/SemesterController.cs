using Microsoft.AspNetCore.Mvc;
using PROG_POE.Models;
using System.Linq;

namespace PROG_POE.Controllers
{
    public class SemesterController : Controller
    {
        //variable to get db context
        private readonly StudentStudyContext _context;

        //constructor which instantialises the db context for access to semesters
        public SemesterController(StudentStudyContext context)
        {
            _context = context;
        }

        //get action for index page of semester model
        public IActionResult Index()
        {
            return View(_context.Semesters.ToList());
        }

        //Get action for DetailsInput page
        [HttpGet]
        public IActionResult DetailsInput()
        {
            return View();
        }

        //Post action calculates study hours for each module based on number of weeks and credits as well as class hours and adds to database
        [HttpPost]
        public IActionResult DetailsInput(int NumWeeks, DateTime StartDate)
        {
            Semester.NumWeeks = NumWeeks;
              var userModules = _context.Modules.Where(m => m.Username == HttpContext.Session.GetString("UserLoggedIn")).ToList();

            foreach (Module module in userModules)
            {
                bool semesterExists = _context.Semesters.Any(s => s.Code == module.Code);
                if (semesterExists) { break; }
                else 
                {
                    int studyHrs = ((module.Credits * 10) / NumWeeks) - module.ClassHours;
                    for (int i = 0; i < NumWeeks; i++)
                    {
                        Semester semester = new Semester()
                        {
                            Code = module.Code,
                            Username = TempData["UsernameAsTempData"].ToString(),
                            StudyHours = studyHrs,
                            WeekNum = (i + 1)
                        };
                        _context.Semesters.Add(semester);
                    }
                }
                
            }
            _context.SaveChanges();
            return RedirectToAction("Index","StudyHours");
        }


        public IActionResult CalculateHours()
        {
            int numWeeks = Semester.NumWeeks;
            var userModules = _context.Modules.Where(m => m.Username == HttpContext.Session.GetString("UserLoggedIn")).ToList();

            foreach (Module module in userModules)
            {
                bool semesterExists = _context.Semesters.Any(s => s.Code == module.Code);
                if (semesterExists) { break; }
                else
                {
                    int studyHrs = ((module.Credits * 10) / numWeeks) - module.ClassHours;
                    for (int i = 0; i < numWeeks; i++)
                    {
                        Semester semester = new Semester()
                        {
                            Code = module.Code,
                            Username = TempData["UsernameAsTempData"].ToString(),
                            StudyHours = studyHrs,
                            WeekNum = (i + 1)
                        };
                        _context.Semesters.Add(semester);
                    }
                }

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "StudyHours");
        }
    }
}
