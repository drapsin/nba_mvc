using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace nba_mvc.ViewModels
{
    public class RefereeCreateViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Experience { get; set; }
        public string Licence { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
