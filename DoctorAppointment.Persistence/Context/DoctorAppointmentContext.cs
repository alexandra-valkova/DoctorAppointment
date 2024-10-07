using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Persistence.EntityConfigurations;
using DoctorAppointment.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.Context
{
    public sealed class DoctorAppointmentContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
                                                                     IdentityUserClaim<int>, ApplicationUserRole,
                                                                     IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                                                     IdentityUserToken<int>>
    {
        public DoctorAppointmentContext(DbContextOptions<DoctorAppointmentContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<ApplicationRole>(new RoleConfiguration());
            modelBuilder.ApplyConfiguration<ApplicationUser>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Appointment>(new AppointmentConfiguration());

            modelBuilder.Seed();
        }
    }
}
