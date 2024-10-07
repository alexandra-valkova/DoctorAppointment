using DoctorAppointment.Domain.Interfaces.Repositories;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository AppointmentRepository { get; }

        Task SaveAsync();
    }
}
