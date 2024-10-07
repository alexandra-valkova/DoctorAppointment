using System.Linq.Expressions;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.Domain.Specifications.Appointments;

namespace DoctorAppointment.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> AppointmentExistsAsync(int id)
        {
            return _repository.ExistsAsync(id);
        }

        public Task<bool> AppointmentExistsAsync(Expression<Func<Appointment, bool>> predicate)
        {
            return _repository.ExistsAsync(predicate);
        }

        public async Task<IReadOnlyList<Appointment>> ListAppointmentsAsync(AppointmentStatus? status = null,
                                                                            int? patientId = null,
                                                                            int? doctorId = null)
        {
            return await _repository.ListAsync(new AppointmentListSpecification(status, patientId, doctorId));
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id, new AppointmentSpecification());
        }

        public async Task<int> CreateAppointmentAsync(Appointment appointment)
        {
            return await _repository.AddAsync(appointment);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _repository.EditAsync(appointment);
        }

        public async Task<bool> UpdateAppointmentStatusAsync(int id, AppointmentStatus status)
        {
            if (await _repository.GetByIdAsync(id) is Appointment appointment)
            {
                appointment.Status = status;
                await _repository.EditAsync(appointment);

                return true;
            }

            return false;
        }

        public async Task<Appointment?> DeleteAppointmentAsync(int id)
        {
            return await _repository.GetByIdAsync(id) is Appointment appointment ? await _repository.DeleteAsync(appointment) : null;
        }
    }
}
