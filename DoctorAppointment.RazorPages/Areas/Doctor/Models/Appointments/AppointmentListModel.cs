using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.RazorPages.Areas.Doctor.Models.Appointments
{
    public class AppointmentListModel
    {
        public IList<AppointmentModel> Appointments { get; set; }

        public AppointmentStatus Status { get; set; }

        public AppointmentListModel(IList<AppointmentModel> appointments, AppointmentStatus status)
        {
            Appointments = appointments;
            Status = status;
        }
    }
}
