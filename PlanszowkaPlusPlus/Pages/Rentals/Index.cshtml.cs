using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Pages.Rentals
{
    public class IndexModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public IndexModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Rent> Rent { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Rent = await _context.Rentals
                .Include(r => r.Game)
                .Include(r => r.Member).ToListAsync();
        }
    }
}
