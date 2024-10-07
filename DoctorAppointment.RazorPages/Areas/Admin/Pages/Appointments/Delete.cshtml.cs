using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Appointments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Appointments
{
    public class DeleteModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public DeleteModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public AppointmentReadModel Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _appointmentService.GetAppointmentByIdAsync(id.Value) is Appointment appointment)
            {
                Appointment = appointment;
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id.HasValue && await _appointmentService.DeleteAppointmentAsync(id.Value) is not null)
            {
                return RedirectToPage("./Index");
            }

            return NotFound();
        }
    }
}
