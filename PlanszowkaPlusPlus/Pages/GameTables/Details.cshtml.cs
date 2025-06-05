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

namespace PlanszowkaPlusPlus.Pages.GameTables
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Employee,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public DetailsModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public GameTable GameTable { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gametable = await _context.GameTables.FirstOrDefaultAsync(m => m.Id == id);
            if (gametable == null)
            {
                return NotFound();
            }
            else
            {
                GameTable = gametable;
            }
            return Page();
        }
    }
}
