using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanszowkaPlusPlus.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace PlanszowkaPlusPlus.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly Data.AppDbContext _context;

        public LoginModel(Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Credential Credential { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Sprawdzenie jako Employee
            var employee = _context.Employees.SingleOrDefault(e => e.Email == Credential.Username);
            if (employee != null)
            {
                var passwordCheck = new PasswordHasher<Employee>();
                var result = passwordCheck.VerifyHashedPassword(employee, employee.PasswordHash, Credential.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, employee.Email),
                        new Claim(ClaimTypes.Name, employee.Name),
                        new Claim(ClaimTypes.Role, "Employee")
                    };

                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("MyCookieAuth", principal);
                    return RedirectToPage("/Index"); // strona pracownika
                }
            }

            // Sprawdzenie jako Member
            var member = _context.Members.SingleOrDefault(m => m.Email == Credential.Username);
            if (member != null)
            {
                var passwordCheck = new PasswordHasher<Member>();
                var result = passwordCheck.VerifyHashedPassword(member, member.PasswordHash, Credential.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, member.Email),
                        new Claim(ClaimTypes.Name, member.Name),
                        new Claim(ClaimTypes.Surname, member.Surname),
                        new Claim(ClaimTypes.Role, "Member")
                    };

                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("MyCookieAuth", principal);
                    return RedirectToPage("/MemberUI/Index"); // strona klienta
                }
            }

            ModelState.AddModelError(string.Empty, "Nieprawidłowy login lub hasło.");
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
