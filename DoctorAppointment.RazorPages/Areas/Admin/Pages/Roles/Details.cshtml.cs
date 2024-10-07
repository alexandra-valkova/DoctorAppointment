using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Roles;
using DoctorAppointment.RazorPages.Areas.Admin.Models.UserRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DetailsModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public RoleModel Role { get; set; } = default!;

        [Display(Name = "Users")]
        public IEnumerable<UserRoleModel> UserRoles { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _roleManager.Roles.Include(role => role.UserRoles)
                                                       .ThenInclude(userRole => userRole.User)
                                                       .FirstOrDefaultAsync(role => role.Id == id) is ApplicationRole role)
            {
                UserRoles = role.UserRoles.Select(userRole => new UserRoleModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    UserId = userRole.UserId,
                    Username = userRole.User.UserName
                }).OrderBy(userRole => userRole.Username);

                Role = role;

                return Page();
            }

            return NotFound();
        }
    }
}
