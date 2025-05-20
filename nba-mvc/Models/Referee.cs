using System.ComponentModel.DataAnnotations;

namespace nba_mvc.Models
{
    public class Referee : BaseId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Range(0, 100, ErrorMessage = "The value must be positive")] 
        public string Age { get; set; }
        public string Experience { get; set; }
        public string Licence { get; set; }
        public string? ImageUrl { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
