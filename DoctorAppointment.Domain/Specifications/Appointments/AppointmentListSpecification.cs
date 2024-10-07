using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Domain.Specifications.Appointments
{
    public class AppointmentListSpecification : AppointmentSpecification
    {
        public AppointmentListSpecification(AppointmentStatus? status = null,
                                            int? patientId = null,
                                            int? doctorId = null,
                                            bool includePatient = true,
                                            bool includeDoctor = true) : base(includePatient, includeDoctor)
        {
            if (status.HasValue) AddFilter(appointment => appointment.Status == status);

            if (patientId.HasValue) AddFilter(appointment => appointment.PatientId == patientId);

            if (doctorId.HasValue) AddFilter(appointment => appointment.DoctorId == doctorId);

            SetOrderBy(appointment => appointment.Date);
        }
    }
}
