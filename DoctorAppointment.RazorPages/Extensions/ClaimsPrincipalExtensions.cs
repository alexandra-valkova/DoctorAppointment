using System.Security.Claims;

namespace DoctorAppointment.RazorPages.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out int userId)
                    ? userId
                    : throw new InvalidCastException("User Id is unavailable.");
        }
    }
}
