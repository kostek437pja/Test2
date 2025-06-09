using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test2.Exceptions;
using Test2.Models;
using Test2.Models.DTO;

namespace Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IDBService _dBService;

        public PlayersController(IDBService dBService)
        {
            _dBService = dBService;
        }

        [HttpGet("{id}/matches")]
        public async Task<IActionResult> GetPlayerMatches(int id)
        {
            try
            {
                return Ok(await _dBService.GetPlayerMatches(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] AddPlayerDTO addPlayerDto)
        {
            try
            {
                await _dBService.AddPlayer(addPlayerDto);
                return Created();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
