using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.RazorPages.Areas.Doctor.Models.Appointments
{
    public class AppointmentModel
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string? Patient { get; set; }

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
                Patient = appointment.Patient.FullName,
                Date = appointment.Date,
                Status = appointment.Status
            };
        }

        public static implicit operator Appointment(AppointmentModel model)
        {
            return new Appointment
            {
                Id = model.Id,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                Date = model.Date,
                Status = model.Status
            };
        }
    }
}
