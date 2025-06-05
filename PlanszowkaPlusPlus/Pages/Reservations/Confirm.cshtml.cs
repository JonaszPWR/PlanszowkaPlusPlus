using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using Microsoft.AspNetCore.Authorization;


namespace PlanszowkaPlusPlus.Pages.Reservations
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Employee,Admin")]
    public class ConfirmModel : PageModel
    {
        private readonly AppDbContext _context;

        public ConfirmModel(AppDbContext context)
        {
            _context = context;
        }

        public List<ReservationRequest> Requests { get; set; } = new();

        public async Task OnGetAsync()
        {
            Requests = await _context.ReservationRequests.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int requestId, string action)
        {
            var request = await _context.ReservationRequests.FindAsync(requestId);

            if (request == null)
                return RedirectToPage("/Error");

            if (action == "confirm")
            {
                var member = await _context.Members.FirstOrDefaultAsync(m => m.Email == request.Email);
                if (member == null)
                {
                    ModelState.AddModelError("", "Nie znaleziono członka o podanym adresie email.");
                    return RedirectToPage(); // lub zwróć Page() jeśli chcesz pokazać błąd
                }

                var reservation = new Reservation
                {
                    ReservationDate = request.Date,
                    TimeStart = request.TimeStart,
                    TimeEnd = request.TimeEnd,
                    TableId = request.TableId,
                    MemberId = member.Id
                };

                _context.Reservations.Add(reservation);
            }

            // W obu przypadkach usuwamy żądanie (zatwierdzone lub odrzucone)
            _context.ReservationRequests.Remove(request);
            await _context.SaveChangesAsync();

            return RedirectToPage(); // wraca na listę
        }
    }
}
