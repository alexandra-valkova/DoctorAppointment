using Microsoft.AspNetCore.Identity;

namespace DoctorAppointment.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>, IEntity<int>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName => $"{FirstName} {LastName}";

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
