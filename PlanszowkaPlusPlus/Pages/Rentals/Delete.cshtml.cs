using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Pages.Rentals
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Employee,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public DeleteModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rent Rent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rentals.FirstOrDefaultAsync(m => m.Id == id);

            if (rent == null)
            {
                return NotFound();
            }
            else
            {
                Rent = rent;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rentals.FindAsync(id);
            if (rent != null)
            {
                Rent = rent;
                _context.Rentals.Remove(Rent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
