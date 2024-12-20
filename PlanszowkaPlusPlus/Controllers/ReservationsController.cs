using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ReservationsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            using var transaction = _appDbContext.Database.BeginTransaction();
            _appDbContext.Reservations.Add(reservation);
            var table = _appDbContext.GameTables.Single(t => t.Id == reservation.TableId);
            //to na razie pozwala na dwie rezerwacje w tym samym czasie
            //if (table != null)
            //{
            //    if (null == table.Reservations)
            //    {
            //        table.Reservations = new List<Reservation>();
            //    }
            //    table.Reservations.Append(reservation);
            //}
            await _appDbContext.SaveChangesAsync();
            transaction.Commit();

            return Ok(reservation);
        }
    }
}
