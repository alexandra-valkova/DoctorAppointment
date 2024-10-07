using System.Linq.Expressions;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Domain.Interfaces.Services
{
    public interface IAppointmentService
    {
        Task<bool> AppointmentExistsAsync(int id);

        Task<bool> AppointmentExistsAsync(Expression<Func<Appointment, bool>> predicate);

        Task<IReadOnlyList<Appointment>> ListAppointmentsAsync(AppointmentStatus? status = null, int? patientId = null, int? doctorId = null);

        Task<Appointment?> GetAppointmentByIdAsync(int id);

        Task<int> CreateAppointmentAsync(Appointment appointment);

        Task UpdateAppointmentAsync(Appointment appointment);

        Task<bool> UpdateAppointmentStatusAsync(int id, AppointmentStatus status);

        Task<Appointment?> DeleteAppointmentAsync(int id);
    }
}
