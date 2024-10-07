using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<UserModel> Users { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.Cast<UserModel>().ToListAsync();
        }
    }
}
