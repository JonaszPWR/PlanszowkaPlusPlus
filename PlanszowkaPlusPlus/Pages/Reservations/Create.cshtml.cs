using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using Microsoft.EntityFrameworkCore;


namespace PlanszowkaPlusPlus.Pages.Reservations
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class CreateModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public CreateModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
{
            var tables = _context.GameTables.ToList();
            var members = _context.Members.ToList();

            if (tables.Count == 0 || members.Count == 0)
            {
                // Optionally handle the case where there are no tables or members in the database
                ModelState.AddModelError(string.Empty, "No tables or members available to create a reservation.");
            }

            ViewData["TableId"] = new SelectList(tables, "Id", "Id");
            ViewData["MemberId"] = new SelectList(members, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool tableExists = await _context.GameTables.AnyAsync(t => t.Id == Reservation.TableId);
            bool memberExists = await _context.Members.AnyAsync(m => m.Id == Reservation.MemberId);

            if (!tableExists)
            {
                ModelState.AddModelError("Reservation.TableId", "The selected table no longer exists.");
                return Page();
            }

            if (!memberExists)
            {
                ModelState.AddModelError("Reservation.MemberId", "The selected member no longer exists.");
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
