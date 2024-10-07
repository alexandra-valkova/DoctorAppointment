using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Persistence.Context;

namespace DoctorAppointment.Persistence.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DoctorAppointmentContext context) : base(context)
        {
        }
    }
}
