using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace nba_mvc.ViewModels
{
    public class CoachEditViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public string History { get; set; }

        [Required]
        public Guid TeamId { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? CurrentImageUrl { get; set; }
    }
}
