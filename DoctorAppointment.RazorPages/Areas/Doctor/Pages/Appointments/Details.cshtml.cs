using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Doctor.Models.Appointments;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorAppointment.RazorPages.Areas.Doctor.Pages.Appointments
{
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public DetailsModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public AppointmentModel Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _appointmentService.GetAppointmentByIdAsync(id.Value) is Appointment appointment)
            {
                if (appointment.DoctorId != User.GetUserId())
                {
                    return Unauthorized();
                }

                Appointment = appointment;

                return Page();
            }

            return NotFound();
        }
    }
}
