using DoctorAppointment.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointment.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(user => user.FirstName).HasMaxLength(256).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(256).IsRequired();

            builder.HasMany(user => user.UserRoles)
                   .WithOne(userRole => userRole.User)
                   .HasForeignKey(userRole => userRole.UserId)
                   .IsRequired();
        }
    }
}
