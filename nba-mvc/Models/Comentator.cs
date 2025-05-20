using System.ComponentModel.DataAnnotations;

namespace nba_mvc.Models
{
    public class Comentator : BaseId
    {
        public string Name { get; set; }
        public string Channel { get; set; }
        public string Contact { get; set; }
        public string News { get; set; }
        public string Ranking { get; set; }
        public string Site { get; set; }
        public string City { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
