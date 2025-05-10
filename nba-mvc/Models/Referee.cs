namespace nba_mvc.Models
{
    public class Referee : BaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Experience { get; set; }
        public string Licence { get; set; }

    }
}
