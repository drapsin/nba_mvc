namespace nba_mvc.Models
{
    public class Arena : BaseId
    {
        public string ArenaName { get; set; }
        public string ArenaLocation { get; set; }
        public int Capacity { get; set; }
    }
}
