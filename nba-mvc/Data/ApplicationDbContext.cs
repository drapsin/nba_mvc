using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using nba_mvc.Models;

namespace nba_mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<nba_mvc.Models.Team> Team { get; set; } = default!;
        public DbSet<nba_mvc.Models.Comentator> Comentator { get; set; } = default!;
        public DbSet<nba_mvc.Models.Player> Player { get; set; } = default!;
    }
}
