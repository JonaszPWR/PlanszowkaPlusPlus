using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanszowkaPlusPlus.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PlanszowkaPlusPlus.Data;

namespace PlanszowkaPlusPlus.Pages.Account
{
    public class AdminLoginModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Admin> _hasher;

        public AdminLoginModel(AppDbContext context, IPasswordHasher<Admin> hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        [BindProperty]
        public LoginCredential Credential { get; set; } = new();

        public class LoginCredential
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var admin = _context.Admins.FirstOrDefault(a => a.Username == Credential.Username);
            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return Page();
            }

            var result = _hasher.VerifyHashedPassword(admin, admin.PasswordHash, Credential.Password);
            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            return RedirectToPage("/AdminDashboard");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_context.Admins.Any())
            {
                return RedirectToPage("/Account/InitialAdminSetup");
            }

            return Page();
        }
    }
}
