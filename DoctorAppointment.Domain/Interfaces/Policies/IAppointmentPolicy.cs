using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;

namespace DoctorAppointment.Domain.Interfaces.Policies
{
    public interface IAppointmentPolicy
    {
        Task<Result> CanCreateAppointmentAsync(Appointment appointment);

        Task<Result> CanUpdateAppointmentAsync(int id, Appointment appointment);
    }
}