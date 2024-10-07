using DoctorAppointment.Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorAppointment.RazorPages.Extensions
{
    public static class UserListExtensions
    {
        public static SelectList ToSelectList(this IEnumerable<ApplicationUser> users)
        {
            ArgumentNullException.ThrowIfNull(users);

            return new SelectList(users.OrderBy(user => user.FirstName)
                                       .ThenBy(user => user.LastName)
                                       .Select(user => new SelectListItem
                                       {
                                           Value = user.Id.ToString(),
                                           Text = user.FullName,
                                       }), "Value", "Text");
        }

        public static MultiSelectList ToMultiSelectList(this IEnumerable<ApplicationUser> users, IEnumerable<string>? selectedUsers = null)
        {
            ArgumentNullException.ThrowIfNull(users);

            return new MultiSelectList(users.OrderBy(user => user.FirstName)
                                            .ThenBy(user => user.LastName)
                                            .Select(user => new SelectListItem
                                            {
                                                Value = user.Id.ToString(),
                                                Text = user.UserName
                                            }), "Value", "Text", selectedUsers);
        }
    }
}
