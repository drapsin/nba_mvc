using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace nba_mvc.ViewModels
{
    public class RefereeEditViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Experience { get; set; }
        public string Licence { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? CurrentImageUrl { get; set; }
    }
}
