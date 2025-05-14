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

namespace nba_mvc.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public TeamsController(ApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }


        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Team.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["ArenaId"] = new SelectList(_context.Arena, "Id", "ArenaName");
            return View();
        }


        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ArenaId"] = new SelectList(_context.Arena, "Id", "ArenaName", model.ArenaId);
                return View(model);
            }

            string imageUrl = await _cloudinaryService.UploadImageAsync(model.ProfileImage);

            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                City = model.City,
                Site = model.Site,
                Sponsor = model.Sponsor,
                News = model.News,
                Ranking = model.Ranking,
                Contact = model.Contact,
                ArenaId = model.ArenaId,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Team.Add(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var team = await _context.Team.FindAsync(id);
            if (team == null) return NotFound();

            var model = new TeamEditViewModel
            {
                Id = team.Id,
                Name = team.Name,
                City = team.City,
                Site = team.Site,
                Sponsor = team.Sponsor,
                News = team.News,
                Ranking = team.Ranking,
                Contact = team.Contact,
                ArenaId = team.ArenaId,
                CurrentImageUrl = team.ImageUrl
            };

            ViewData["ArenaId"] = new SelectList(_context.Arena, "Id", "ArenaName", team.ArenaId);
            return View(model);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TeamEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["ArenaId"] = new SelectList(_context.Arena, "Id", "ArenaName", model.ArenaId);
                return View(model);
            }
            var team = await _context.Team.FindAsync(id);
            if (team == null) return NotFound();
            if (model.ProfileImage != null)
            {
                string newImageUrl = await _cloudinaryService.UploadImageAsync(model.ProfileImage);
                team.ImageUrl = newImageUrl;
            }
            team.Name = model.Name;
            team.City = model.City;
            team.Site = model.Site;
            team.Sponsor = model.Sponsor;
            team.News = model.News;
            team.Ranking = model.Ranking;
            team.Contact = model.Contact;
            team.ArenaId = model.ArenaId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(Guid id)
        {
            return _context.Team.Any(e => e.Id == id);
        }
    }
}
