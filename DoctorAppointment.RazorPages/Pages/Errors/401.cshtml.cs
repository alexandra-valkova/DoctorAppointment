using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Pages.Errors
{
    [AllowAnonymous]
    public class UnauthorizedModel : PageModel
    {
        public IActionResult OnGet(string? ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return Page();
        }
    }
}
