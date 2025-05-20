using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nba_mvc.Data;
using nba_mvc.Models;
using nba_mvc.Services;
using nba_mvc.ViewModels;

namespace nba_mvc.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public PlayersController(ApplicationDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var players = _context.Player.Include(p => p.Team);
            return View(await players.ToListAsync());
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", model.TeamId);
                return View(model);
            }

            string imageUrl = await _cloudinaryService.UploadImageAsync(model.ProfileImage);

            var player = new Player
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                Position = model.Position,
                Height = model.Height,
                Weight = model.Weight,
                Manager = model.Manager,
                Sponsor = model.Sponsor,
                News = model.News,
                TeamId = model.TeamId,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Add(player);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var player = await _context.Player
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (player == null) return NotFound();

            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var player = await _context.Player.FindAsync(id);
            if (player == null) return NotFound();

            var model = new PlayerEditViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Age = player.Age,
                Position = player.Position,
                Height = player.Height,
                Weight = player.Weight,
                Manager = player.Manager,
                Sponsor = player.Sponsor,
                News = player.News,
                TeamId = player.TeamId,
                CurrentImageUrl = player.ImageUrl
            };

            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", player.TeamId);
            return View(model);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PlayerEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", model.TeamId);
                return View(model);
            }

            var player = await _context.Player
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null) return NotFound();

            // Apply changes
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.Age = model.Age;
            player.Position = model.Position;
            player.Height = model.Height;
            player.Weight = model.Weight;
            player.Manager = model.Manager;
            player.Sponsor = model.Sponsor;
            player.News = model.News;
            player.TeamId = model.TeamId;
            player.RowVersion = model.RowVersion;

            if (model.ProfileImage != null)
            {
                string newImageUrl = await _cloudinaryService.UploadImageAsync(model.ProfileImage);
                player.ImageUrl = newImageUrl;
            }

            try
            {
                _context.Player.Update(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                var dbEntry = await _context.Player
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (dbEntry == null)
                    return NotFound();

                ModelState.AddModelError("", "Another admin has modified this record. Your changes were not saved. Please review the current data.");

                // Re-populate Team dropdown and reset current image
                ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Name", model.TeamId);
                model.CurrentImageUrl = dbEntry.ImageUrl;
                model.RowVersion = dbEntry.RowVersion;

                return View(model);
            }
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var player = await _context.Player
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (player == null) return NotFound();

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(Guid id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
    }
}
