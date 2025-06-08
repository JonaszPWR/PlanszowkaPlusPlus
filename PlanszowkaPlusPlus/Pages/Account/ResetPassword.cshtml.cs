using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly AppDbContext _context;

        public ResetPasswordModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Token { get; set; } = string.Empty;

        [BindProperty]
        public string NewPassword { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            var tokenEntry = _context.PasswordResetTokens.FirstOrDefault(t => t.Token == Token && t.Expiration > DateTime.UtcNow);
            if (tokenEntry == null)
            {
                ModelState.AddModelError("", "Token wygasł lub jest nieprawidłowy.");
                return Page();
            }

            var email = tokenEntry.Email;
            var updated = false;

            // Admin
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email);
            if (admin != null)
            {
                admin.PasswordHash = new PasswordHasher<Admin>().HashPassword(admin, NewPassword);
                _context.Admins.Update(admin);
                updated = true;
            }

            // Employee
            var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
            if (employee != null)
            {
                employee.PasswordHash = new PasswordHasher<Employee>().HashPassword(employee, NewPassword);
                _context.Employees.Update(employee);
                updated = true;
            }

            // Member
            var member = _context.Members.FirstOrDefault(m => m.Email == email);
            if (member != null)
            {
                member.PasswordHash = new PasswordHasher<Member>().HashPassword(member, NewPassword);
                _context.Members.Update(member);
                updated = true;
            }

            if (updated)
            {
                _context.PasswordResetTokens.Remove(tokenEntry);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Account/Login");
            }

            ModelState.AddModelError("", "Nie znaleziono użytkownika.");
            return Page();
        }
    }
}
