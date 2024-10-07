using DoctorAppointment.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointment.Persistence.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(role => role.Description).HasMaxLength(256);

            builder.HasMany(role => role.UserRoles)
                   .WithOne(userRole => userRole.Role)
                   .HasForeignKey(userRole => userRole.RoleId)
                   .IsRequired();
        }
    }
}
