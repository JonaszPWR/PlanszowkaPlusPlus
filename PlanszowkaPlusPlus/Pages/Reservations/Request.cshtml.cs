using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Pages.Client
{
    public class RequestReservationModel : PageModel
    {
        private readonly AppDbContext _context;

        public RequestReservationModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReservationInput Input { get; set; } = new();

        public List<SelectListItem> Tables { get; set; } = new();

        public void OnGet()
        {
            Tables = _context.GameTables
                .Where(t => t.IsFree)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = $"Stolik {t.Number}" })
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            TempData["ReservationRequest"] = System.Text.Json.JsonSerializer.Serialize(Input);
            return RedirectToPage("/Reservations/Confirm");
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
