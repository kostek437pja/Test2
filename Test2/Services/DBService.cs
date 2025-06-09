using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Exceptions;
using Test2.Models;
using Test2.Models.DTO;

namespace Test2;

public class DBService : IDBService
{
    private readonly DatabaseContext _context;

    public DBService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PlayerMatchesDTO> GetPlayerMatches(int id)
    {
        var player = await _context.Players
            .Include(pm => pm.PlayerMatches)
            .ThenInclude(pm => pm.Match)
            .ThenInclude(m => m.Tournament)
            .Include(pm => pm.PlayerMatches)
            .ThenInclude(pm => pm.Match)
            .ThenInclude(m => m.Map)
            .FirstOrDefaultAsync(p => p.PlayerId == id);

        if (player == null)
        {
            throw new NotFoundException("Player not found");
        }

        List<MatchDto> matches = new List<MatchDto>();
        foreach (var playerMatch in player.PlayerMatches)
        {
            matches.Add(new MatchDto()
            {
                Torunament = playerMatch.Match.Tournament.Name,
                Map = playerMatch.Match.Map.Name,
                Date = playerMatch.Match.MatchDate,
                MVPs = playerMatch.MVPs,
                Rating = playerMatch.Rating,
                Team1Score = playerMatch.Match.Team1Score,
                Team2Score = playerMatch.Match.Team2Score,
            });
        }

        return new PlayerMatchesDTO()
        {
            PlayerId = player.PlayerId,
            FirstName = player.FirstName,
            LastName = player.LastName,
            BirthDate = player.BirthDate,
            Matches = matches
        };
    }

    public async Task AddPlayer(AddPlayerDTO playerDto)
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {   
            var player = new Player()
            {
                FirstName = playerDto.FirstName,
                LastName = playerDto.LastName,
                BirthDate = playerDto.BirthDate,
                
            };
            
            var createdPlayer = await _context.Players.AddAsync(player);
            
            foreach (var matchDto in playerDto.matches)
            {
                var match = _context.Matches.FirstOrDefault(m => m.MatchId == matchDto.MatchId);
                if (match == null)
                {
                    throw new NotFoundException($"Match with id {matchDto.MatchId} not found");
                }
                
                var PlayerMatch = new PlayerMatch()
                {
                    MatchId = matchDto.MatchId,
                    Player = createdPlayer.Entity,
                    MVPs = matchDto.MVPs,
                    Rating = matchDto.Rating
                };
                
                await _context.PlayerMatches.AddAsync(PlayerMatch);
            }
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}