using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlanszowkaPlusPlus.Pages.AdminDashboard
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
