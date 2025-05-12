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
    public class RefereesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RefereesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Referees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Referee.ToListAsync());
        }

        // GET: Referees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referee == null)
            {
                return NotFound();
            }

            return View(referee);
        }

        // GET: Referees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Referees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Age,Experience,Licence,Id,CreatedAt")] Referee referee)
        {
            if (ModelState.IsValid)
            {
                referee.Id = Guid.NewGuid();
                _context.Add(referee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referee);
        }

        // GET: Referees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referee.FindAsync(id);
            if (referee == null)
            {
                return NotFound();
            }
            return View(referee);
        }

        // POST: Referees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,Age,Experience,Licence,Id,CreatedAt")] Referee referee)
        {
            if (id != referee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefereeExists(referee.Id))
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
            return View(referee);
        }

        // GET: Referees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referee == null)
            {
                return NotFound();
            }

            return View(referee);
        }

        // POST: Referees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var referee = await _context.Referee.FindAsync(id);
            if (referee != null)
            {
                _context.Referee.Remove(referee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefereeExists(Guid id)
        {
            return _context.Referee.Any(e => e.Id == id);
        }
    }
}
