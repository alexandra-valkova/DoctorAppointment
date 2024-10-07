using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Patient.Models.Appointments;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Patient.Pages.Appointments
{
    public class DeleteModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public DeleteModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public AppointmentModel Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _appointmentService.GetAppointmentByIdAsync(id.Value) is Appointment appointment)
            {
                if (appointment.PatientId != User.GetUserId())
                {
                    return Unauthorized();
                }

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
