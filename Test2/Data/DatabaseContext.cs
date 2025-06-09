using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Data;

public class DatabaseContext : DbContext
{   
    
    public DbSet<Map> Maps { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerMatch> PlayerMatches { get; set; }
    public DbSet<Match> Matches { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().HasData(new List<Player>
        {
            new Player(){PlayerId = 1, FirstName = "John", LastName = "Doe", BirthDate = DateTime.Now.AddYears(-25)},
            new Player(){PlayerId = 2, FirstName = "John", LastName = "Doe", BirthDate = DateTime.Now.AddYears(-25)},
        });
        
        modelBuilder.Entity<Map>().HasData(new List<Map>
        {
            new Map(){MapId = 1, Name = "Map 1", Type = "Town"},
            new Map(){MapId = 2, Name = "Map 2", Type = "Rural"},
        });

        modelBuilder.Entity<Tournament>().HasData(new List<Tournament>
        {
            new Tournament(){TournamentId = 1, Name = "Torunament 1", StartDate = DateTime.Parse("2025-05-01"), EndDate = DateTime.Parse("2025-05-02")},
            new Tournament(){TournamentId = 2, Name = "Torunament 2", StartDate = DateTime.Parse("2025-06-01"), EndDate = DateTime.Parse("2025-06-02")}
        });

        modelBuilder.Entity<Match>().HasData(new List<Match>
        {
            new Match(){MatchId = 1, MapId = 1, TournamentId = 1, MatchDate = DateTime.Parse("2025-05-01"), Team1Score = 15, Team2Score = 20},
            new Match(){MatchId = 2, MapId = 2, TournamentId = 2, MatchDate = DateTime.Parse("2025-06-01"), Team1Score = 16, Team2Score = 12}
        });
        modelBuilder.Entity<PlayerMatch>().HasData(new List<PlayerMatch>
        {
            new PlayerMatch() { MatchId = 1, PlayerId = 1, MVPs = 2, Rating = 51.40 },
            new PlayerMatch() { MatchId = 2, PlayerId = 2, MVPs = 2, Rating = 50.50 },
        });
    }
}