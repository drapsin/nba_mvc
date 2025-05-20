namespace nba_mvc.ViewModels
{
    public class PlayerEditViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Manager { get; set; }
        public string Sponsor { get; set; }
        public string News { get; set; }
        public Guid TeamId { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? CurrentImageUrl { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
