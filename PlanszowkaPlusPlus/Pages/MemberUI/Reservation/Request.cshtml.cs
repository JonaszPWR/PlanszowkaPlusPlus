using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace PlanszowkaPlusPlus.Pages.MemberUI.Reservation
{
    public class RequestReservationModel : PageModel
    {
        private readonly AppDbContext _context;

        public RequestReservationModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReservationRequest Input { get; set; } = new();

        public List<SelectListItem> Tables { get; set; } = new();

        public void OnGet()
        {
            LoadTables();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            LoadTables(); // upewnij się, że lista się ładuje również po błędzie

            if (!ModelState.IsValid)
                return Page();

            // Sprawdź czy podany e-mail istnieje w bazie członków
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Email == Input.Email);
            if (member == null)
            {
                ModelState.AddModelError("Input.Email", "Nie znaleziono użytkownika o podanym adresie e-mail.");
                return Page();
            }

            // Dodaj zgłoszenie rezerwacji (bez MemberId w modelu)
            _context.ReservationRequests.Add(Input);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Rezerwacja została zgłoszona i oczekuje na potwierdzenie.";
            return RedirectToPage(); // zostaje na tej samej stronie
        }

        private void LoadTables()
        {
            Tables = _context.GameTables
                .Where(t => t.IsFree)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Stolik {t.Number}"
                })
                .ToList();
        }
    }
}
