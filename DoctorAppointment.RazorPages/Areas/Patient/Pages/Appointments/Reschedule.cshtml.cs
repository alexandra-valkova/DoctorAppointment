using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Patient.Models.Appointments;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Patient.Pages.Appointments
{
    public class RescheduleModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentPolicy _appointmentPolicy;

        public RescheduleModel(IAppointmentService appointmentService, IAppointmentPolicy appointmentPolicy)
        {
            _appointmentService = appointmentService;
            _appointmentPolicy = appointmentPolicy;
        }

        [BindProperty]
        public AppointmentRescheduleModel Appointment { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Appointment is null)
            {
                return Page();
            }

            Result canRescheduleResult = await _appointmentPolicy.CanUpdateAppointmentAsync(Appointment.Id, Appointment);

            if (canRescheduleResult.IsFailure)
            {
                ModelState.AddModelError(string.Empty, canRescheduleResult.Error.Description);
                return Page();
            }

            try
            {
                Appointment.Status = AppointmentStatus.Pending;
                await _appointmentService.UpdateAppointmentAsync(Appointment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _appointmentService.AppointmentExistsAsync(Appointment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
