using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Appointments;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public IndexModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IList<AppointmentReadModel> Appointments { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Appointments = (await _appointmentService.ListAppointmentsAsync()).ToList().ConvertAll<AppointmentReadModel>(a => a);
        }
    }
}
