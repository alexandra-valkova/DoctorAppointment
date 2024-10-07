using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.UserRoles;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Users;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DeleteModel(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public UserModel ApplicationUser { get; set; } = default!;

        [Display(Name = "Roles")]
        public IEnumerable<UserRoleModel> UserRoles { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _userManager.FindByIdAsync(id.Value.ToString()) is ApplicationUser user)
            {
                ApplicationUser = user;
                await PopulateRoles();

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id.HasValue && await _userManager.FindByIdAsync(id.Value.ToString()) is ApplicationUser user)
            {
                try
                {
                    IdentityResult deleteUserResult = await _userManager.DeleteAsync(user);

                    if (!deleteUserResult.Succeeded)
                    {
                        ModelState.AddModelIdentityErrors(deleteUserResult);
                        return Page();
                    }

                    return RedirectToPage("./Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Unable to delete user because appointments are associated with it.");

                    ApplicationUser = user;
                    await PopulateRoles();

                    return Page();
                }

            }

            return NotFound();
        }

        private async Task PopulateRoles()
        {
            UserRoles = (await _userManager.GetRolesAsync(ApplicationUser)).Select(role => new UserRoleModel
            {
                RoleId = _roleManager.FindByNameAsync(role).Id,
                RoleName = role,
                UserId = ApplicationUser.Id,
                Username = ApplicationUser.Username
            });
        }
    }
}
