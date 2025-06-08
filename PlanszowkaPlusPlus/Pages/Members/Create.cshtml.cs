using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace PlanszowkaPlusPlus.Pages.Members
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Employee,Admin")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Member> _hasher;

        public CreateModel(AppDbContext context, IPasswordHasher<Member> hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Member.RegistrationDate = DateTime.Now;
            Member.PasswordHash = _hasher.HashPassword(Member, Password);
            _context.Members.Add(Member);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
