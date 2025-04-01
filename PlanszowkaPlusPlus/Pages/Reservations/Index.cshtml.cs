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

namespace PlanszowkaPlusPlus.Pages.Reservations
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class IndexModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public IndexModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public DateOnly? FilterDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool ShowHiddenRecords { get; set; } = false;
        public async Task OnGetAsync()
        {
            
            var query = _context.Reservations
                .Include(r => r.GameTable)
                .Include(r => r.Member)
                .AsQueryable();

            if (FilterDate.HasValue)
            {
                query = query.Where(r => r.ReservationDate == FilterDate.Value);
            }

            if (!ShowHiddenRecords)
            {
                query = query.Where(r => r.IsArchived == false);
            }

            Reservation = await query.ToListAsync();
        }
    }
}
