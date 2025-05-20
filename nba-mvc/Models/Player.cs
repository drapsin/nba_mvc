using System.ComponentModel.DataAnnotations;

namespace nba_mvc.Models
{
    public class Player : BaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Range(0, 100, ErrorMessage = "The value must be positive")]
        public int Age { get; set; }
        public string Position { get; set; }
        public Guid TeamId { get; set; }    
        public Team? Team { get; set; }
        [Range(150, 230, ErrorMessage = "Please enter a valid height")]
        public string Height { get; set; }
        [Range(70, 200, ErrorMessage = "Please enter a valid weight")]
        public string Weight { get; set; }
        public string Manager { get; set; }
        public string Sponsor { get; set; }
        public string News { get; set; }
        public string? ImageUrl { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
