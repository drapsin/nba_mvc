using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nba_mvc.Models
{
    public class Game : BaseId
    {
        public DateTime GameDate { get; set; }

        [ForeignKey("HomeTeam")]
        public Team? HomeTeam { get; set; }

        [ForeignKey ("AwayTeam")]
        public Team? AwayTeam { get; set; }
        public Guid LocationId { get; set; }
        public Arena? Location { get; internal set; }
        public string GameResult { get; set; }
        public string Sponsor { get; set; }
        public ICollection<ActionEvent> ActionEvents { get; set; } = new List<ActionEvent>();
        public ICollection<Player> Players { get; set; }
    }
}
