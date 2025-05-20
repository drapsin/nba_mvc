using System.ComponentModel.DataAnnotations;

namespace nba_mvc.Models
{
    public class Coach : BaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Range(0, 100, ErrorMessage = "The value must be positive")]
        public int Age { get; set; }
        public string History { get; set; }
        public Guid TeamId { get; set; }
        public Team? Team { get; set; }
        public string? ImageUrl { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
