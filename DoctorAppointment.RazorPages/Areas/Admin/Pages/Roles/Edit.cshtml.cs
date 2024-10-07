using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Roles;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _roleManager.FindByIdAsync(id.Value.ToString()) is ApplicationRole role)
            {
                Role = role;

                await PopulateUsersAsync();
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Role is null || UserRoles is null)
            {
                await PopulateUsersAsync();
                return Page();
            }

            try
            {
                ApplicationRole? role = await _roleManager.FindByIdAsync(Role.Id.ToString());

                if (role is null)
                {
                    return NotFound();
                }

                role.Name = Role.Name;
                role.Description = Role.Description;

                IdentityResult updateRoleResult = await _roleManager.UpdateAsync(role);

                if (!updateRoleResult.Succeeded)
                {
                    ModelState.AddModelIdentityErrors(updateRoleResult);

                    await PopulateUsersAsync();
                    return Page();
                }

                foreach (ApplicationUser user in await _userManager.Users.ToListAsync())
                {
                    bool isUserInRole = await _userManager.IsInRoleAsync(user, Role.Name);
                    bool isUserSelected = UserRoles.Contains(user.Id.ToString());

                    if (isUserInRole ^ isUserSelected)
                    {
                        IdentityResult addOrRemoveUserRoleResult = isUserSelected
                            ? await _userManager.AddToRoleAsync(user, Role.Name)
                            : await _userManager.RemoveFromRoleAsync(user, Role.Name);

                        if (!addOrRemoveUserRoleResult.Succeeded)
                        {
                            ModelState.AddModelIdentityErrors(addOrRemoveUserRoleResult);

                            await PopulateUsersAsync();
                            return Page();
                        }
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _roleManager.RoleExistsAsync(Role.Name))
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

        private async Task PopulateUsersAsync()
        {
            UserRoles = (await _userManager.GetUsersInRoleAsync(Role.Name)).Select(user => user.Id.ToString());
            Users = _userManager.Users.ToMultiSelectList(selectedUsers: UserRoles);
        }
    }
}
