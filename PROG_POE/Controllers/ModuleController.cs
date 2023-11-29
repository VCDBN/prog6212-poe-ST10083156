using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG_POE.Models;

namespace PROG_POE.Controllers
{
    public class ModuleController : Controller
    {
        private readonly StudentStudyContext _context;

        public ModuleController(StudentStudyContext context)
        {
            _context = context;
        }

        // GET: Module
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserLoggedIn") != null) 
            {
               
                var studentStudyContext = _context.Modules.Where(m => m.Username == HttpContext.Session.GetString("UserLoggedIn"));
                return View(await studentStudyContext.ToListAsync());
            }
            else
            {
                TempData["LoginRequired"] = "Login required for access to modules!";
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Module/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(x => x.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Module/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: Module/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Credits,ClassHours")] Module @module)
        {
            if (ModelState.IsValid)
            {
                if (!ModuleExists(module.Code))
                {
                    module.Username = HttpContext.Session.GetString("UserLoggedIn");
                    _context.Add(@module);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Error = "Module code already exists in database!";
                    return View();
                }
                
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", @module.Username);
            return View(@module);
        }

        // GET: Module/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", @module.Username);
            return View(@module);
        }

        // POST: Module/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name,Credits,ClassHours,Username")] Module @module)
        {
            if (id != @module.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", @module.Username);
            return View(@module);
        }

        // GET: Module/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(x => x.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                _context.Modules.Remove(@module);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(string id)
        {

            return _context.Modules.Any(e => e.Code == id && e.Username == HttpContext.Session.GetString("UserLoggedIn"));
        }
    }
}
