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
                Employee? employee = _context.Employees.Single(e => e.Email == Credential.Username);
                var passwordCheck = new PasswordHasher<Employee>();
                var result = passwordCheck.VerifyHashedPassword(employee, employee.PasswordHash, Credential.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    //Creating the security context
                    var claims = new List<Claim> { new Claim(ClaimTypes.Email, Credential.Username) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("MyCookieAuth", principal);
                    //Redirect to home page
                    Console.WriteLine("Redirecting");
                    return RedirectToPage("/Index");
                }
                else
                {
                    Console.WriteLine("It didn't work");
                }
            }
            catch (InvalidOperationException e)
            {   
                Console.WriteLine("Exception thrown:" + e.Message);
                //browser popup for login error?
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
