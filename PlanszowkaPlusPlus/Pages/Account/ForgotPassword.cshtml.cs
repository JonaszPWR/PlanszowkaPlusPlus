using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using PlanszowkaPlusPlus.Services;

namespace PlanszowkaPlusPlus.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public ForgotPasswordModel(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ModelState.AddModelError("", "Podaj e-mail.");
                return Page();
            }

            var token = Guid.NewGuid().ToString();
            var entry = new PasswordResetToken
            {
                Email = Email,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };

            _context.PasswordResetTokens.Add(entry);
            await _context.SaveChangesAsync();

            var link = Url.Page("/Account/ResetPassword", null, new { token = token }, Request.Scheme);
            await _emailService.SendEmailAsync(Email, "Reset hasła", $"Kliknij, aby zresetować hasło: {link}");

            TempData["Success"] = "E-mail z linkiem został wysłany.";
            return RedirectToPage();
        }
    }
}
