using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Doctor.Models.Appointments;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static DoctorAppointment.Domain.Entities.AppointmentStatus;

namespace DoctorAppointment.RazorPages.Areas.Doctor.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public IndexModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public AppointmentListModel PendingAppointments { get; set; } = default!;

        public AppointmentListModel ApprovedAppointments { get; set; } = default!;

        public AppointmentListModel DeclinedAppointments { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IEnumerable<AppointmentModel> appointments = (await _appointmentService.ListAppointmentsAsync(doctorId: User.GetUserId())).Select(appointment => (AppointmentModel)appointment);

            PendingAppointments = new(FilterByStatus(appointments, Pending), status: Pending);

            ApprovedAppointments = new(FilterByStatus(appointments, Approved), status: Approved);

            DeclinedAppointments = new(FilterByStatus(appointments, Declined), status: Declined);
        }

        private static List<AppointmentModel> FilterByStatus(IEnumerable<AppointmentModel> appointments, AppointmentStatus status)
        {
            return appointments.Where(appointment => appointment.Status == status).ToList();
        }
    }
}
