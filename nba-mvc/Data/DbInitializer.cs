using Microsoft.AspNetCore.Identity;
using nba_mvc.Models;

namespace nba_mvc.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static void SeedData(ApplicationDbContext context)
        {
            var positions = new[] { "PG", "SG", "SF", "PF", "C" };

            if (!context.Arena.Any())
            {
                var arenas = new List<Arena>();
                for (int i = 1; i <= 5; i++)
                {
                    arenas.Add(new Arena
                    {
                        Id = Guid.NewGuid(),
                        ArenaName = $"Arena {i}",
                        ArenaLocation = $"City {i}",
                        Capacity = 10000 + i * 1000
                    });
                }
                context.Arena.AddRange(arenas);
                context.SaveChanges();
            }

            if (!context.Team.Any())
            {
                var arenaIds = context.Arena.Select(a => a.Id).ToList();
                var teams = new List<Team>();
                for (int i = 1; i <= 10; i++)
                {
                    teams.Add(new Team
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Team {i}",
                        City = $"City {i}",
                        Site = $"www.team{i}.com",
                        Sponsor = $"Sponsor {i}",
                        News = $"News for Team {i}",
                        Ranking = i.ToString(),
                        Contact = $"contact@team{i}.com",
                        ArenaId = arenaIds[i % arenaIds.Count],
                        ImageUrl = null
                    });
                }
                context.Team.AddRange(teams);
                context.SaveChanges();
            }

            if (!context.Player.Any())
            {
                var teamIds = context.Team.Select(t => t.Id).ToList();
                var players = new List<Player>();
                var rnd = new Random();

                for (int i = 1; i <= 100; i++)
                {
                    players.Add(new Player
                    {
                        Id = Guid.NewGuid(),
                        FirstName = $"Player{i}",
                        LastName = $"Last{i}",
                        Age = rnd.Next(19, 35),
                        Position = positions[rnd.Next(positions.Length)],
                        TeamId = teamIds[i % teamIds.Count],
                        Height = rnd.Next(170, 220).ToString(),
                        Weight = rnd.Next(70, 130).ToString(),
                        Manager = $"Manager {i}",
                        Sponsor = $"Sponsor {i}",
                        News = $"Player {i} signs deal.",
                        ImageUrl = null
                    });
                }
                context.Player.AddRange(players);
                context.SaveChanges();
            }
        }
    }
}
