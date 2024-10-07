using System.Reflection;
using DoctorAppointment.Application.Behaviors;
using DoctorAppointment.Application.Services;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.Domain.Policies;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAppointment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddScoped<IUserPolicy, UserPolicy>();
            services.AddScoped<IAppointmentPolicy, AppointmentPolicy>();

            return services;
        }
    }
}
