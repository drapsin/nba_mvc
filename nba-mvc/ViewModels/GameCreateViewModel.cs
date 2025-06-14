using Microsoft.AspNetCore.Mvc.Rendering;
using nba_mvc.Models;
using System.ComponentModel;

namespace nba_mvc.ViewModels
{
    public class GameCreateViewModel
    {
        public DateTime GameDate { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<SelectListItem> Arenas { get; set; } = new();
        public Arena LocationId { get; set; }
        public string GameResult { get; set; }
        public string Sponsor { get; set; }
    }
}
