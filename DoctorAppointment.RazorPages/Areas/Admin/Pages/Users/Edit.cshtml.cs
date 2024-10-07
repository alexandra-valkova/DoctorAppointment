using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Users;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EditModel(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _userManager.FindByIdAsync(id.Value.ToString()) is ApplicationUser user)
            {
                ApplicationUser = user;

                await PopulateRolesAsync();
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || ApplicationUser is null || UserRoles is null)
            {
                await PopulateRolesAsync();
                return Page();
            }

            try
            {
                ApplicationUser? user = await _userManager.FindByIdAsync(ApplicationUser.Id.ToString());

                if (user is null)
                {
                    return NotFound();
                }

                user.FirstName = ApplicationUser.FirstName;
                user.LastName = ApplicationUser.LastName;
                user.UserName = ApplicationUser.Username;
                user.Email = ApplicationUser.Email;

                IdentityResult updateUserResult = await _userManager.UpdateAsync(user);

                if (!updateUserResult.Succeeded)
                {
                    ModelState.AddModelIdentityErrors(updateUserResult);

                    await PopulateRolesAsync();
                    return Page();
                }

                if (await _userManager.GetRolesAsync(user) is IList<string> roles && roles.Count > 0)
                {
                    IdentityResult removeUserRolesResult = await _userManager.RemoveFromRolesAsync(user, roles);

                    if (!removeUserRolesResult.Succeeded)
                    {
                        ModelState.AddModelIdentityErrors(removeUserRolesResult);

                        await PopulateRolesAsync();
                        return Page();
                    }
                }

                if (UserRoles.Any())
                {
                    IdentityResult addUserRolesResult = await _userManager.AddToRolesAsync(user, UserRoles);

                    if (!addUserRolesResult.Succeeded)
                    {
                        ModelState.AddModelIdentityErrors(addUserRolesResult);

                        await PopulateRolesAsync();
                        return Page();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _userManager.FindByIdAsync(ApplicationUser.Id.ToString()) is null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task PopulateRolesAsync()
        {
            UserRoles = await _userManager.GetRolesAsync(ApplicationUser);
            Roles = _roleManager.Roles.ToMultiSelectList(selectedRoles: UserRoles);
        }
    }
}
