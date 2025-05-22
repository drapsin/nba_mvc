using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nba_mvc.Data;
using nba_mvc.Models;
using nba_mvc.ViewModels;
using nba_mvc.Services;

namespace nba_mvc.Controllers
{
    public class RefereesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;

        public RefereesController(ApplicationDbContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: Referees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Referee.ToListAsync());
        }

        // GET: Referees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var referee = await _context.Referee.FirstOrDefaultAsync(m => m.Id == id);
            if (referee == null) return NotFound();

            return View(referee);
        }

        // GET: Referees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Referees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RefereeCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            string? imageUrl = await _imageService.UploadAsync(model.ProfileImage);

            var referee = new Referee
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Experience = model.Experience,
                Licence = model.Licence,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Add(referee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Referees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var referee = await _context.Referee.FindAsync(id);
            if (referee == null) return NotFound();

            var model = new RefereeEditViewModel
            {
                Id = referee.Id,
                FirstName = referee.FirstName,
                LastName = referee.LastName,
                Age = referee.Age,
                Experience = referee.Experience,
                Licence = referee.Licence,
                CurrentImageUrl = referee.ImageUrl
            };

            return View(model);
        }

        // POST: Referees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RefereeEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return View(model);

            var referee = await _context.Referee
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (referee == null) return NotFound();

            referee.FirstName = model.FirstName;
            referee.LastName = model.LastName;
            referee.Age = model.Age;
            referee.Experience = model.Experience;
            referee.Licence = model.Licence;
            referee.RowVersion = model.RowVersion;

            if (model.ProfileImage != null)
            {
                string? newImageUrl = await _imageService.UploadAsync(model.ProfileImage);
                referee.ImageUrl = newImageUrl;
            }

            try
            {
                _context.Referee.Update(referee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                var dbEntry = await _context.Referee
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (dbEntry == null)
                    return NotFound();

                ModelState.AddModelError("", "Another admin has modified this record. Your changes were not saved. Please review the updated information.");

                model.RowVersion = dbEntry.RowVersion;
                model.CurrentImageUrl = dbEntry.ImageUrl;

                return View(model);
            }
        }

        // GET: Referees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var referee = await _context.Referee.FirstOrDefaultAsync(m => m.Id == id);
            if (referee == null) return NotFound();

            return View(referee);
        }

        // POST: Referees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var referee = await _context.Referee.FindAsync(id);
            if (referee != null) _context.Referee.Remove(referee);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefereeExists(Guid id)
        {
            return _context.Referee.Any(e => e.Id == id);
        }
    }
}
