using DoctorAppointment.Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorAppointment.RazorPages.Extensions
{
    public static class RoleListExtensions
    {
        public static MultiSelectList ToMultiSelectList(this IEnumerable<ApplicationRole> roles, IEnumerable<string>? selectedRoles = null)
        {
            ArgumentNullException.ThrowIfNull(roles);

            return new MultiSelectList(roles.Select(role => new SelectListItem
            {
                Value = role.Name,
                Text = $"{role.Name} - {role.Description}"
            }), "Value", "Text", selectedRoles);
        }
    }
}
