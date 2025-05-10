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
    public class ComentatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comentators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comentator.ToListAsync());
        }

        // GET: Comentators/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentator = await _context.Comentator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentator == null)
            {
                return NotFound();
            }

            return View(comentator);
        }

        // GET: Comentators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comentators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Channel,Contact,News,Ranking,Site,City,Id")] Comentator comentator)
        {
            if (ModelState.IsValid)
            {
                comentator.Id = Guid.NewGuid();
                _context.Add(comentator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comentator);
        }

        // GET: Comentators/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentator = await _context.Comentator.FindAsync(id);
            if (comentator == null)
            {
                return NotFound();
            }
            return View(comentator);
        }

        // POST: Comentators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Channel,Contact,News,Ranking,Site,City,Id,CreatedAt")] Comentator comentator)
        {
            if (id != comentator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentatorExists(comentator.Id))
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
            return View(comentator);
        }

        // GET: Comentators/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentator = await _context.Comentator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentator == null)
            {
                return NotFound();
            }

            return View(comentator);
        }

        // POST: Comentators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comentator = await _context.Comentator.FindAsync(id);
            if (comentator != null)
            {
                _context.Comentator.Remove(comentator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentatorExists(Guid id)
        {
            return _context.Comentator.Any(e => e.Id == id);
        }
    }
}
