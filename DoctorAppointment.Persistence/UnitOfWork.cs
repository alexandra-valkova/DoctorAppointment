using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Persistence.Context;

namespace DoctorAppointment.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DoctorAppointmentContext _context;

        public IAppointmentRepository AppointmentRepository { get; private set; }

        public UnitOfWork(DoctorAppointmentContext context, IAppointmentRepository appointmentRepository)
        {
            _context = context;
            AppointmentRepository = appointmentRepository;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async void Dispose() => await _context.DisposeAsync();
    }
}
