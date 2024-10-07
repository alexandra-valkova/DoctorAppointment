using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Doctor.Models.Appointments;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.RazorPages.Areas.Doctor.Pages.Appointments
{
    public class ReviewModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public ReviewModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public AppointmentModel Appointment { get; set; } = default!;

        [FromForm]
        public ReviewAction Review { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Appointment is null)
            {
                return Page();
            }

            try
            {
                Appointment.Status = Review switch
                {
                    ReviewAction.Approve => AppointmentStatus.Approved,
                    ReviewAction.Decline => AppointmentStatus.Declined,
                    _ => throw new NotSupportedException()
                };

                await _appointmentService.UpdateAppointmentAsync(Appointment);
            }
            catch (NotSupportedException)
            {
                return Page();
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

    public enum ReviewAction
    {
        Approve = 0,
        Decline = 1
    }
}
