using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Roles;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty]
        public RoleModel Role { get; set; } = default!;

        public MultiSelectList Users { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Users")]
        public IEnumerable<string> UserRoles { get; set; } = default!;

        public IActionResult OnGet()
        {
            PopulateUsers();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Role is null || UserRoles is null)
            {
                PopulateUsers();
                return Page();
            }

            IdentityResult createRoleResult = await _roleManager.CreateAsync(Role);

            if (!createRoleResult.Succeeded)
            {
                ModelState.AddModelIdentityErrors(createRoleResult);

                PopulateUsers();
                return Page();
            }

            if (UserRoles.Any())
            {
                foreach (string userId in UserRoles)
                {
                    ApplicationUser? user = await _userManager.FindByIdAsync(userId);

                    if (user is null)
                    {
                        ModelState.AddModelError(string.Empty, "User not found: cannot add user to role!");

                        PopulateUsers();
                        return Page();
                    }

                    IdentityResult addUserToRoleResult = await _userManager.AddToRoleAsync(user, Role.Name);

                    if (!addUserToRoleResult.Succeeded)
                    {
                        ModelState.AddModelIdentityErrors(addUserToRoleResult);

                        PopulateUsers();
                        return Page();
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private void PopulateUsers()
        {
            Users = _userManager.Users.ToMultiSelectList();
        }
    }
}
