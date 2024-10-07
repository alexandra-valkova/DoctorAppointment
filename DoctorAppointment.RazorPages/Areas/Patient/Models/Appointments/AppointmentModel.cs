using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.RazorPages.Areas.Patient.Models.Appointments
{
    public class AppointmentModel
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string? Doctor { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }

        public static implicit operator AppointmentModel(Appointment appointment)
        {
            return new AppointmentModel
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Doctor = appointment.Doctor.FullName,
                Date = appointment.Date,
                Status = appointment.Status
            };
        }
    }
}
