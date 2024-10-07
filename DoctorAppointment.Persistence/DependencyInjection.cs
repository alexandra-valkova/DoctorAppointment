using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Persistence.Context;
using DoctorAppointment.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAppointment.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(connectionString);

            return services.AddDbContext<DoctorAppointmentContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static IdentityBuilder AddIdentityStorageProvider(this IdentityBuilder identity)
        {
            ArgumentNullException.ThrowIfNull(identity);

            return identity.AddEntityFrameworkStores<DoctorAppointmentContext>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            return services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }
    }
}
