using Test2.Models;
using Test2.Models.DTO;

namespace Test2;

public interface IDBService
{
    Task<PlayerMatchesDTO> GetPlayerMatches(int id);
    Task AddPlayer(AddPlayerDTO playerDto);
}