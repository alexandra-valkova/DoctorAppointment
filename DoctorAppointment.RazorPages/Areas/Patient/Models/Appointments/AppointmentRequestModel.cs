using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.RazorPages.Attributes;

namespace DoctorAppointment.RazorPages.Areas.Patient.Models.Appointments
{
    public class AppointmentRequestModel
    {
        [Required]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }

        [Required]
        [MinDateToday]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public static implicit operator Appointment(AppointmentRequestModel model)
        {
            return new Appointment
            {
                DoctorId = model.DoctorId,
                Date = model.Date
            };
        }
    }
}
