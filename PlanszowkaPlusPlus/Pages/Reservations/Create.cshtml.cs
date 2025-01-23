using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Pages.Reservations
{
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

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
