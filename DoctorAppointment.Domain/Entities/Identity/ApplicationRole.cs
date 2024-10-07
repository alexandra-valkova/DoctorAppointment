using Microsoft.AspNetCore.Identity;

namespace DoctorAppointment.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>, IEntity<int>
    {
        public string? Description { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
