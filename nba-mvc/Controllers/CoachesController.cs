using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nba_mvc.Data;
using nba_mvc.Models;
using nba_mvc.Services;
using nba_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace nba_mvc.Controllers
{
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        public CoachesController(ApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
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
        public async Task<IActionResult> Create(CoachCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", model.TeamId);
                return View(model);
            }
            string imageUrl = await _cloudinaryService.UploadImageAsync(model.ProfileImage);

            var coach = new Coach
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                History = model.History,
                TeamId = model.TeamId,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow
            };
            _context.Add(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var coach = await _context.Coach.FindAsync(id);
            if (coach == null) return NotFound();

            var model = new CoachEditViewModel
            {
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                Age = coach.Age,
                History = coach.History,
                TeamId = coach.TeamId,
                CurrentImageUrl = coach.ImageUrl
            };
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", coach.TeamId);
            return View(model);
        }


        // POST: Coaches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CoachEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", model.TeamId);
                return View(model);
            }
            var coach = await _context.Coach.FindAsync(id);
            if (coach == null) return NotFound();

            if (model.ProfileImage != null)
            {
                string newImageUrl = await _cloudinaryService.UploadImageAsync(model.ProfileImage);
                coach.ImageUrl = newImageUrl;
            }
            coach.FirstName = model.FirstName;
            coach.LastName = model.LastName;
            coach.Age = model.Age;
            coach.History = model.History;
            coach.TeamId = model.TeamId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
