using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public RentsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRent(Rent rent)
        {
            using var transaction = _appDbContext.Database.BeginTransaction();

            var game = await _appDbContext.Games.FindAsync(rent.GameId);
            if (game == null)
            {
                return NotFound($"Game with ID {rent.GameId} not found.");
            }

            if (game.AvailableNumber <= 0)
            {
                return BadRequest("No copies of the game are available for rent.");
            }

            game.AvailableNumber--;

            _appDbContext.Rentals.Add(rent);
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            return Ok(rent);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetRents([FromQuery] int? memberId, [FromQuery] int? gameId)
        //{
        //    IQueryable<Rent> rentsQuery = _appDbContext.Rentals.Include(r => r.Game).Include(r => r.Member);

        //    if (memberId.HasValue)
        //    {
        //        rentsQuery = rentsQuery.Where(r => r.MemberId == memberId);
        //    }

        //    if (gameId.HasValue)
        //    {
        //        rentsQuery = rentsQuery.Where(r => r.GameId == gameId);
        //    }

        //    var rents = await rentsQuery.ToListAsync();
        //    return Ok(rents);
        //}

        //[HttpPut("{id}/return")]
        //public async Task<IActionResult> ReturnRent(int id)
        //{
        //    using var transaction = _appDbContext.Database.BeginTransaction();

        //    var rent = await _appDbContext.Rentals.Include(r => r.Game).FirstOrDefaultAsync(r => r.Id == id);
        //    if (rent == null)
        //    {
        //        return NotFound($"Rent with ID {id} not found.");
        //    }

        //    if (rent.ReturnDate.HasValue)
        //    {
        //        return BadRequest("This rent has already been returned.");
        //    }

        //    rent.ReturnDate = DateOnly.FromDateTime(DateTime.Now);

        //    rent.Game.AvailableNumber++;

        //    await _appDbContext.SaveChangesAsync();

        //    await transaction.CommitAsync();
        //    return Ok(rent);
        //}
    }
}
