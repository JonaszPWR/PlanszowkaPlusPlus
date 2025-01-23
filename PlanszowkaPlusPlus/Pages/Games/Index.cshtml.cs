using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public IndexModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get; set; } = default!;
        public List<string> Categories { get; set; } = new(); // Store unique categories

        [BindProperty(SupportsGet = true)]
        public string? SelectedCategory { get; set; } // Selected category from the dropdown

        public async Task OnGetAsync()
        {
            // Retrieve all unique categories for the dropdown
            Categories = await _context.Games
                .Select(g => g.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            // Start with all games
            var gamesQuery = _context.Games.AsQueryable();

            // Filter by category if one is selected
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                gamesQuery = gamesQuery.Where(g => g.Category == SelectedCategory);
            }

            Game = await gamesQuery.ToListAsync();
        }
    }
}
