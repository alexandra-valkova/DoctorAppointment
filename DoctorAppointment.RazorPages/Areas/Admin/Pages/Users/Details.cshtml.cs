using System.ComponentModel.DataAnnotations;
using System.Data;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.UserRoles;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public UserModel ApplicationUser { get; set; } = default!;

        [Display(Name = "Roles")]
        public IEnumerable<UserRoleModel> UserRoles { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _userManager.Users.Include(user => user.UserRoles)
                                                       .ThenInclude(userRole => userRole.Role)
                                                       .SingleOrDefaultAsync(user => user.Id == id) is ApplicationUser user)
            {
                UserRoles = user.UserRoles.Select(userRole => new UserRoleModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    RoleId = userRole.RoleId,
                    RoleName = userRole.Role.Name
                }).OrderBy(userRole => userRole.RoleName);

                ApplicationUser = user;

                return Page();
            }

            return NotFound();
        }
    }
}
