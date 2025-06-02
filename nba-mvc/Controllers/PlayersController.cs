using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using nba_mvc.Data;
using nba_mvc.Models;
using nba_mvc.Services;
using nba_mvc.ViewModels;

namespace nba_mvc.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageUploader _imageUploader;
        private readonly IMemoryCache _cache;

        public PlayersController(ApplicationDbContext context, IImageUploader imageUploader, IMemoryCache cache)
        {
            _context = context;
            _imageUploader = imageUploader; 
            _cache = cache;
        }

        // GET: Players
        public async Task<IActionResult> Index(string sortOrder, string searchString, Guid? teamId, string position, int page = 1)
        {
            int pageSize = 10;

            // ViewData for filters and sorting UI
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;
            ViewData["SelectedTeam"] = teamId;
            ViewData["SelectedPosition"] = position;

            // Cached list of teams
            ViewData["Teams"] = _cache.GetOrCreate("TeamList", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return new SelectList(
                    _context.Team
                        .AsNoTracking()
                        .OrderBy(t => t.Name)
                        .ToList(),
                    "Id",
                    "Name"
                );
            });

            // Sorting parameters
            ViewData["PositionSortParm"] = string.IsNullOrEmpty(sortOrder) ? "position_desc" : "";
            ViewData["HeightSortParm"] = sortOrder == "height" ? "height_desc" : "height";
            ViewData["WeightSortParm"] = sortOrder == "weight" ? "weight_desc" : "weight";
            ViewData["TeamSortParm"] = sortOrder == "team" ? "team_desc" : "team";

            // Base query with Include to allow accessing Team.Name in projection
            var playersQuery = _context.Player
                .Include(p => p.Team)
                .AsNoTracking();

            // Search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                playersQuery = playersQuery.Where(p =>
                    p.FirstName.Contains(searchString) || p.LastName.Contains(searchString));
            }

            if (teamId.HasValue)
            {
                playersQuery = playersQuery.Where(p => p.TeamId == teamId.Value);
            }

            if (!string.IsNullOrEmpty(position))
            {
                playersQuery = playersQuery.Where(p => p.Position == position);
            }

            // Sorting
            playersQuery = sortOrder switch
            {
                "position_desc" => playersQuery.OrderByDescending(p => p.Position),
                "height" => playersQuery.OrderBy(p => p.Height),
                "height_desc" => playersQuery.OrderByDescending(p => p.Height),
                "weight" => playersQuery.OrderBy(p => p.Weight),
                "weight_desc" => playersQuery.OrderByDescending(p => p.Weight),
                "team" => playersQuery.OrderBy(p => p.Team.Name),
                "team_desc" => playersQuery.OrderByDescending(p => p.Team.Name),
                _ => playersQuery.OrderBy(p => p.LastName),
            };

            // Pagination + projection to ViewModel
            int totalPlayers = await playersQuery.CountAsync();
            var players = await playersQuery
                .Select(p => new PlayerListViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = p.Position,
                    Age = p.Age,
                    Height = p.Height,
                    Weight = p.Weight,
                    Manager = p.Manager,
                    Sponsor = p.Sponsor,
                    News = p.News,
                    CreatedAt = p.CreatedAt,
                    TeamName = p.Team.Name,
                    ImageUrl = p.ImageUrl
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalPlayers / (double)pageSize);

            // Cached list of normalized positions
            ViewData["Positions"] = _cache.GetOrCreate("PlayerPositions", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return _context.Player
                    .AsNoTracking()
                    .Select(p => p.Position.Trim().ToUpper())
                    .Where(p => !string.IsNullOrEmpty(p))
                    .Distinct()
                    .OrderBy(p => p)
                    .ToList();
            });

            return View(players);
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

            var imageUrl = await _imageUploader.UploadImageAsync(model.ProfileImage);

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
                var newImageUrl = await _imageUploader.UploadImageAsync(model.ProfileImage);
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
        
        // API endpoint to get players for external use
        [AllowAnonymous]

        [HttpGet("/api/players")]
        public IActionResult GetPlayers()
        {
            var players = _context.Player
                .Include(p => p.Team)
                .Select(p => new {
                    Id = p.Id,
                    Name = p.FirstName + " " + p.LastName,
                    Position = p.Position,
                    Team = p.Team != null ? p.Team.Name : null,
                    ImageUrl = p.ImageUrl
                })
                .ToList();
            return Json(players);
        }

    }
}
