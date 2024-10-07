using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Roles;
using DoctorAppointment.RazorPages.Areas.Admin.Models.UserRoles;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty]
        public RoleModel Role { get; set; } = default!;

        public IEnumerable<UserRoleModel> Users { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _roleManager.FindByIdAsync(id.Value.ToString()) is ApplicationRole role)
            {
                Role = role;

                Users = (await _userManager.GetUsersInRoleAsync(Role.Name)).Select(user => new UserRoleModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    RoleId = Role.Id,
                    RoleName = Role.Name
                });

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id.HasValue && await _roleManager.FindByIdAsync(id.Value.ToString()) is ApplicationRole role)
            {
                IdentityResult deleteRoleResult = await _roleManager.DeleteAsync(role);

                if (!deleteRoleResult.Succeeded)
                {
                    ModelState.AddModelIdentityErrors(deleteRoleResult);
                    return Page();
                }

                return RedirectToPage("./Index");
            }

            return NotFound();
        }
    }
}
