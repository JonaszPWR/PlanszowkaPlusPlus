using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using Microsoft.AspNetCore.Authorization;

namespace PlanszowkaPlusPlus.Pages.Client
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Member")]
    public class GamesListModel : PageModel
    {
        private readonly AppDbContext _context;

        public GamesListModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Game> Games { get; set; } = new List<Game>();

        public async Task OnGetAsync()
        {
            Games = await _context.Games
                .OrderBy(g => g.Title)
                .ToListAsync();
        }
    }
}
