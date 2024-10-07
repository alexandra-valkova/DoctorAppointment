using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace DoctorAppointment.RazorPages.Areas.Account.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public CredentialsModel Credentials { get; set; } = default!;

        [FromForm]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public IActionResult OnGet(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? ReturnUrl)
        {
            if (!ModelState.IsValid || Credentials is null)
            {
                return Page();
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(Credentials.Username, Credentials.Password, isPersistent: RememberMe, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            return !string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl)
                    ? LocalRedirect(ReturnUrl)
                    : RedirectToPage("./Index");
        }
    }
}
