using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlanszowkaPlusPlus.Pages.MemberUI
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Member")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
