namespace nba_mvc.ViewModels
{
    public class PlayerListViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }            
        public string Height { get; set; }
        public string Weight { get; set; }
        public string? Manager { get; set; }    
        public string? Sponsor { get; set; }    
        public string? News { get; set; }       
        public DateTime CreatedAt { get; set; } 
        public string? TeamName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
