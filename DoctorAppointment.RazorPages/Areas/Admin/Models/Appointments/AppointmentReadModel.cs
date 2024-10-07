using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Users;

namespace DoctorAppointment.RazorPages.Areas.Admin.Models.Appointments
{
    public class AppointmentReadModel
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public UserModel Patient { get; set; }

        public UserModel Doctor { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }

        public static implicit operator AppointmentReadModel(Appointment appointment)
        {
            return new AppointmentReadModel
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Patient = appointment.Patient,
                Doctor = appointment.Doctor,
                Date = appointment.Date,
                Status = appointment.Status
            };
        }
    }
}
