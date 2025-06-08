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

namespace PlanszowkaPlusPlus.Pages.Rentals
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Employee,Admin")]
    public class CreateModel : PageModel
    {
        
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public CreateModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Rent = new Rent
            {
                RentDate = DateOnly.FromDateTime(DateTime.Today),
                ReturnDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7))
            };
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Rent Rent { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Rentals.Add(Rent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
