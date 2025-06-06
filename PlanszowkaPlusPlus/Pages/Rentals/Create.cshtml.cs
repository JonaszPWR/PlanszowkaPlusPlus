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
        ViewData["GameId"] = new SelectList(_context.Games, "Id", "Id");
        ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public RentDTO RentInfo { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Game? game = await _context.Games.FindAsync(RentInfo.GameId);
            Member? member = await _context.Members.FindAsync(RentInfo.MemberId);

            if (null == game || game.AvailableNumber <= 0)
            {
                ModelState.AddModelError("RentInfo.GameId", "The selected game is not available.");
                return Page();//TODO: separate errors for null and none in storage?
            }

            if (null == member)
            {
                ModelState.AddModelError("RentInfo.MemberId", "The selected member no longer exists.");
                return Page();
            }
            
            _context.Rentals.Add(new Rent
            {
                RentDate = RentInfo.RentDate,
                ReturnDate = RentInfo.ReturnDate,
                MemberId =  RentInfo.MemberId,
                GameId = RentInfo.GameId,
                Member = member,
                Game = game
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
