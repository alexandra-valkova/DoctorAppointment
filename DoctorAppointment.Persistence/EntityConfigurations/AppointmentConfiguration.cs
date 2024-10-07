using DoctorAppointment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorAppointment.Persistence.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.Status).IsRequired();

            builder.HasOne(a => a.Patient).WithMany()
                   .HasForeignKey(a => a.PatientId).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor).WithMany()
                   .HasForeignKey(a => a.DoctorId).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
