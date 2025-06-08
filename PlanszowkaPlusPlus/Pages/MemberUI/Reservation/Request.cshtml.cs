using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace PlanszowkaPlusPlus.Pages.MemberUI.Reservation
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Member")]

public class RequestReservationModel : PageModel
{
    private readonly AppDbContext _context;

    public RequestReservationModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ReservationRequest Input { get; set; } = new();

public void OnGet()
{
    var user = HttpContext.User;

    var firstName = user.FindFirst(ClaimTypes.Name)?.Value ?? "";
    var lastName = user.FindFirst(ClaimTypes.Surname)?.Value ?? "";

    Input = new ReservationRequest
    {
        FullName = $"{firstName} {lastName}".Trim(),
        Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "",
        Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1))
    };
}

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Znajdź członka na podstawie e-maila
        var member = await _context.Members.FirstOrDefaultAsync(m => m.Email == Input.Email);
        if (member == null)
        {
            ModelState.AddModelError("Input.Email", "Nie znaleziono użytkownika o podanym adresie e-mail.");
            return Page();
        }

        // Znajdź pierwszy dostępny wolny stolik
        var table = await _context.GameTables.FirstOrDefaultAsync(t => t.IsFree);
        if (table == null)
        {
            ModelState.AddModelError(string.Empty, "Brak dostępnych stolików.");
            return Page();
        }

        // Utwórz nową prośbę
        var reservationRequest = new ReservationRequest
        {
            FullName = Input.FullName,
            Email = Input.Email,
            Date = Input.Date,
            TimeStart = Input.TimeStart,
            TimeEnd = Input.TimeEnd,
            TableId = table.Id
        };

        _context.ReservationRequests.Add(reservationRequest);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Rezerwacja została zgłoszona i oczekuje na potwierdzenie.";
        return RedirectToPage();
    }
}
}
