using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public GamesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddGame(Game game)
        {
            _appDbContext.Games.Add(game);
            await _appDbContext.SaveChangesAsync();
            return Ok(game);
        }

        [HttpGet]
        public async Task<IActionResult> GetGames([FromQuery] string? status)
        {
            IQueryable<Game> gamesQuery = _appDbContext.Games;

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "available")
                {
                    gamesQuery = gamesQuery.Where(g => g.AvailableNumber > 0);
                }
                else if (status == "borrowed")
                {
                    gamesQuery = gamesQuery.Where(g => g.AvailableNumber == 0);
                }
                else
                {
                    return BadRequest("Invalid status filter. Use 'available' or 'borrowed'.");
                }
            }

            var games = await gamesQuery.ToListAsync();
            return Ok(games);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] Game updatedGame)
        {
            var game = await _appDbContext.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }

            game.Title = updatedGame.Title;
            game.Category = updatedGame.Category;
            game.TotalCount = updatedGame.TotalNumber;
            game.AvailableCount = updatedGame.AvailableNumber;

            await _appDbContext.SaveChangesAsync();
            return Ok(game);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _appDbContext.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }

            _appDbContext.Games.Remove(game);
            await _appDbContext.SaveChangesAsync();
            return Ok($"Game with ID {id} has been deleted.");
        }
    }
}
