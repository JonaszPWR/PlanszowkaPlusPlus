using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanszowkaPlusPlus.Models;
using PlanszowkaPlusPlus.Data;
using Microsoft.AspNetCore.Identity;

namespace PlanszowkaPlusPlus.Pages.Account
{
    public class InitialAdminSetupModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public InitialAdminSetupModel(AppDbContext context, IPasswordHasher<Admin> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public Admin Admin { get; set; } = new Admin();

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            // If there is already at least one Admin, redirect to login
            if (_context.Admins.Any())
            {
                return RedirectToPage("/Account/AdminLogin");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (_context.Admins.Any())
            {
                // Prevent race condition where another admin was added
                return RedirectToPage("/Account/AdminLogin");
            }

            Admin.PasswordHash = _passwordHasher.HashPassword(Admin, Password);
            _context.Admins.Add(Admin);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Account/AdminLogin");
        }
    }
}