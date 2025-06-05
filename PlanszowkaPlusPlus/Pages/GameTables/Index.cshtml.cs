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
    public class IndexModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public IndexModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<GameTable> GameTable { get;set; } = default!;

        public async Task OnGetAsync()
        {
            GameTable = await _context.GameTables.ToListAsync();
        }
    }
}
