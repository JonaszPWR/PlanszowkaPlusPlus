using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using System.Text.Json;

namespace PlanszowkaPlusPlus.Pages.Reservations
{
    public class ConfirmModel : PageModel
    {
        private readonly AppDbContext _context;

        public ConfirmModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReservationInput? ReservationRequest { get; set; }

        public IActionResult OnGet()
        {
            var json = TempData["ReservationRequest"] as string;
            if (json == null)
            {
                return RedirectToPage("/Error");
            }

            ReservationRequest = JsonSerializer.Deserialize<ReservationInput>(json);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (ReservationRequest == null)
                return RedirectToPage("/Error");

            if (action == "confirm")
            {
                var reservation = new Reservation
                {
                    ReservationDate = DateOnly.FromDateTime(ReservationRequest.Date),
                    TimeStart = TimeOnly.FromTimeSpan(ReservationRequest.TimeStart),
                    TimeEnd = TimeOnly.FromTimeSpan(ReservationRequest.TimeEnd),
                    TableId = ReservationRequest.TableId,
                    MemberId = 1 // domyślna wartość, do zmiany jeśli trzeba
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
        public class ReservationInput
        {
            public string FullName { get; set; } = "";
            public string Email { get; set; } = "";
            public DateTime Date { get; set; }
            public TimeSpan TimeStart { get; set; }
            public TimeSpan TimeEnd { get; set; }
            public int TableId { get; set; }
        }
    }
}
