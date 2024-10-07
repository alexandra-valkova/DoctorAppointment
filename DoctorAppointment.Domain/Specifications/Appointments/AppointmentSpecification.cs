using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Domain.Specifications.Appointments
{
    public class AppointmentSpecification : Specification<Appointment>
    {
        public AppointmentSpecification(bool includePatient = true, bool includeDoctor = true)
        {
            if (includePatient) AddInclude(appointment => appointment.Patient);
            if (includeDoctor) AddInclude(appointment => appointment.Doctor);
        }
    }
}
