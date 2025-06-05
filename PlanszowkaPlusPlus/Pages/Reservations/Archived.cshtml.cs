using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using Microsoft.AspNetCore.Authorization;


namespace PlanszowkaPlusPlus.Pages.Reservations
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Employee,Admin")]
    public class ArchivedModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public ArchivedModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservations
                .Include(r => r.GameTable)
                .Include(r => r.Member).ToListAsync();
        }
        public async Task<IActionResult> OnPostUnarchiveAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.IsArchived = false;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
