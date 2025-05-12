using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nba_mvc.Data;
using nba_mvc.Models;

namespace nba_mvc.Controllers
{
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Coach.Include(c => c.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name");
            return View();
        }

        // POST: Coaches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Age,History,TeamId,Id,CreatedAt")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                coach.Id = Guid.NewGuid();
                _context.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", coach.TeamId);
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", coach.TeamId);
            return View(coach);
        }

        // POST: Coaches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,Age,History,TeamId,Id,CreatedAt")] Coach coach)
        {
            if (id != coach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.Id))
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

            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", coach.TeamId);
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var coach = await _context.Coach.FindAsync(id);
            if (coach != null)
            {
                _context.Coach.Remove(coach);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(Guid id)
        {
            return _context.Coach.Any(e => e.Id == id);
        }
    }
}
