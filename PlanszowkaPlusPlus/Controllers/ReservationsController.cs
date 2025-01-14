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
        public async Task<IActionResult> AddReservation(ReservationDTO DTO)
        {
            /*zakładam, że sprawdzenie czy data i godzina nie kolidują
             * z inną rezerwacją będzie na front-endzie, ale można to zmienić
             */
            using var transaction = _appDbContext.Database.BeginTransaction();
            var member = await _appDbContext.Members.FindAsync(DTO.MemberId);
            //var table = await _appDbContext.GameTables.FindAsync(DTO.TableId);
            var table = _appDbContext.GameTables.Single(t => t.Id == DTO.TableId);

            if (null == member)
            {
                return NotFound($"No member with ID {DTO.MemberId}");
            }
            if (null == table)
            {
                return NotFound($"No table with ID {DTO.TableId}");
            }

            var reservation = new Reservation
            {
                Id = DTO.Id,
                ReservationDate = DTO.ReservationDate,
                TimeStart = DTO.TimeStart,
                TimeEnd = DTO.TimeEnd,
                TableId = DTO.TableId,
                GameTable = table,
                MemberId = DTO.MemberId,
                Member = member
            };

            _appDbContext.Reservations.Add(reservation);
            await _appDbContext.SaveChangesAsync();
            transaction.Commit();

            return Ok(reservation);
        }
    }
}
