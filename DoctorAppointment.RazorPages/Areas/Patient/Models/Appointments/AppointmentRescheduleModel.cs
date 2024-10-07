using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.RazorPages.Attributes;

namespace DoctorAppointment.RazorPages.Areas.Patient.Models.Appointments
{
    public class AppointmentRescheduleModel
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [MinDateToday(ErrorMessage = "Date must be equal to or greater than today!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }

        public static implicit operator AppointmentRescheduleModel(Appointment appointment)
        {
            return new AppointmentRescheduleModel
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Date = appointment.Date,
                Status = appointment.Status
            };
        }

        public static implicit operator Appointment(AppointmentRescheduleModel model)
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
