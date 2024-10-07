using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Account.Pages
{
    [AllowAnonymous]
    public class AccessDeniedModel : PageModel
    {
        public IActionResult OnGet(string? ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return Page();
        }
    }
}
