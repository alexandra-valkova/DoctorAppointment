using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public IndexModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IList<RoleModel> Roles { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.Cast<RoleModel>().ToListAsync();
        }
    }
}
