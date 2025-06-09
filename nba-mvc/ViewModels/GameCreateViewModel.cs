using Microsoft.AspNetCore.Mvc.Rendering;
using nba_mvc.Models;
using System.ComponentModel;

namespace nba_mvc.ViewModels
{
    public class GameCreateViewModel
    {
        public DateTime GameDate { get; set; }
        public string GameName { get; set; }
        public List<SelectListItem> Arenas { get; set; } = new();
        public Guid ArenaId { get; set; }
        public string GameResult { get; set; }
        public string Sponsor { get; set; }
    }
}
