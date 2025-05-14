namespace nba_mvc.ViewModels
{
    public class TeamEditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Site { get; set; }
        public string Sponsor { get; set; }
        public string News { get; set; }
        public string Ranking { get; set; }
        public string Contact { get; set; }
        public Guid ArenaId { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? CurrentImageUrl { get; set; }
    }
}
