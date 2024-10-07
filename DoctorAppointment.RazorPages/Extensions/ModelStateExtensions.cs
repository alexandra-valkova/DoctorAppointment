using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DoctorAppointment.RazorPages.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelIdentityErrors(this ModelStateDictionary modelState, IdentityResult identityResult)
        {
            ArgumentNullException.ThrowIfNull(modelState);
            ArgumentNullException.ThrowIfNull(identityResult);

            foreach (IdentityError error in identityResult.Errors)
            {
                modelState.AddModelError(key: string.Empty, errorMessage: error.Description);
            }
        }
    }
}
