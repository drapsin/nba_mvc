namespace nba_mvc.Models
{
    public class Coach : BaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string History { get; set; }
        public Team Team { get; set; }
    }
}
