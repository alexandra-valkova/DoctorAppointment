using DoctorAppointment.Domain.Entities.Identity;

namespace DoctorAppointment.Domain.Entities
{
    public class Appointment : Entity
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }

        public virtual ApplicationUser Patient { get; set; }

        public virtual ApplicationUser Doctor { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending = 0,
        Approved = 1,
        Declined = 2
    }
}
