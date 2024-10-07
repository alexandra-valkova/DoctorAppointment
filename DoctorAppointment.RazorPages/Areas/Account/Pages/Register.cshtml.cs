using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Account.Models;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Account.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public RegistrationModel Registration { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Registration is null)
            {
                return Page();
            }

            ApplicationUser user = Registration;

            IdentityResult registerUserResult = await _userManager.CreateAsync(user, Registration.Password);

            if (!registerUserResult.Succeeded)
            {
                ModelState.AddModelIdentityErrors(registerUserResult);
                return Page();
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToPage("./Index");
        }
    }
}
