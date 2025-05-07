using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using PlanszowkaPlusPlus.Controllers;
using PlanszowkaPlusPlus.Pages.Games;
using System.ComponentModel.DataAnnotations;
using PlanszowkaPlusPlus.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                Employee? employee = _context.Employees.SingleOrDefault(e => e.Email == Credential.Username);
                if (employee == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                //verify password
                var passwordCheck = new PasswordHasher<Employee>();
                var result = passwordCheck.VerifyHashedPassword(employee, employee.PasswordHash, Credential.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    //create security context
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, Credential.Username),
                        new Claim(ClaimTypes.Name, employee.Name),
                        new Claim(ClaimTypes.Role, "User")
                    };

                    var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    //sign in user
                    await HttpContext.SignInAsync("MyCookieAuth", principal);
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
            }
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
