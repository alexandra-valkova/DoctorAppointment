using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppointment.Domain.Policies
{
    public class UserPolicy : IUserPolicy
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserPolicy(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result> CheckIfUserInRoleExists(int id, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new ArgumentException($"Role '{role}' cannot be found!", nameof(role));
            }

            ApplicationUser? user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return Result.Failure(UserErrors.UserNotFound(id));
            }

            if (!await _userManager.IsInRoleAsync(user, role))
            {
                return Result.Failure(UserErrors.UserNotInRole(id, role));
            }

            return Result.Success();
        }
    }
}
