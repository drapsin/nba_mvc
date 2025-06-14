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
        public DbSet<nba_mvc.Models.Player> Player { get; set; } = default!;
        public DbSet<nba_mvc.Models.Arena> Arena { get; set; } = default!;
        public DbSet<nba_mvc.Models.Coach> Coach { get; set; } = default!;
        public DbSet<nba_mvc.Models.Referee> Referee { get; set; } = default!;
        public DbSet<nba_mvc.Models.Game> Game { get; set; } = default!;
        
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActionEvent>()
                .HasOne(a => a.Game)
                .WithMany(g => g.ActionEvents)
                .HasForeignKey(a => a.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActionEvent>()
                .HasOne(a => a.Player)
                .WithMany()
                .HasForeignKey(a => a.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActionEvent>()
               .HasOne(a => a.Team)
               .WithMany()
               .HasForeignKey(a => a.TeamId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany()
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Location)
                .WithMany()
                .HasForeignKey(g => g.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
