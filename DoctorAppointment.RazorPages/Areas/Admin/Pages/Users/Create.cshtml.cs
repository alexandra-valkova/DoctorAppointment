using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Users;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateModel(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public UserModel ApplicationUser { get; set; } = default!;

        public MultiSelectList Roles { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Roles")]
        public IEnumerable<string> UserRoles { get; set; } = default!;

        public IActionResult OnGet()
        {
            PopulateRoles();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || ApplicationUser is null || UserRoles is null)
            {
                PopulateRoles();
                return Page();
            }

            IdentityResult createUserResult = await _userManager.CreateAsync(ApplicationUser);

            if (!createUserResult.Succeeded)
            {
                ModelState.AddModelIdentityErrors(createUserResult);

                PopulateRoles();
                return Page();
            }

            if (UserRoles.Any())
            {
                if (await _userManager.FindByNameAsync(ApplicationUser.Username) is ApplicationUser user)
                {
                    IdentityResult addUserRolesResult = await _userManager.AddToRolesAsync(user, UserRoles);

                    if (!addUserRolesResult.Succeeded)
                    {
                        ModelState.AddModelIdentityErrors(addUserRolesResult);

                        PopulateRoles();
                        return Page();
                    }
                }

                else
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Index");
        }

        private void PopulateRoles()
        {
            Roles = _roleManager.Roles.ToMultiSelectList();
        }
    }
}
