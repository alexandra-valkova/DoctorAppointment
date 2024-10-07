using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoctorAppointment.Persistence.Context
{
    public class DoctorAppointmentContextFactory : IDesignTimeDbContextFactory<DoctorAppointmentContext>
    {
        public DoctorAppointmentContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DoctorAppointmentContext> optionsBuilder = new();

            optionsBuilder.UseSqlServer(connectionString: "Server=.;Database=DoctorAppointmentDB;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true");

            return new DoctorAppointmentContext(optionsBuilder.Options);
        }
    }
}
