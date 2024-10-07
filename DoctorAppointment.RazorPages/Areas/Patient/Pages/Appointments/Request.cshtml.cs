using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Patient.Models.Appointments;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Constants = DoctorAppointment.Domain.Constants;

namespace DoctorAppointment.RazorPages.Areas.Patient.Pages.Appointments
{
    public class RequestModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentPolicy _appointmentPolicy;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestModel(IAppointmentService appointmentService, IAppointmentPolicy appointmentPolicy, UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _appointmentPolicy = appointmentPolicy;
            _userManager = userManager;
        }

        [BindProperty]
        public AppointmentRequestModel Appointment { get; set; } = default!;

        public SelectList Doctors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateDoctors();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Appointment is null)
            {
                return Page();
            }

            Appointment appointment = Appointment;

            appointment.PatientId = User.GetUserId();
            appointment.Status = AppointmentStatus.Pending;

            Result canRequestResult = await _appointmentPolicy.CanCreateAppointmentAsync(appointment);

            if (canRequestResult.IsFailure)
            {
                ModelState.AddModelError(string.Empty, canRequestResult.Error.Description);

                await PopulateDoctors();
                return Page();
            }

            try
            {
                await _appointmentService.CreateAppointmentAsync(appointment);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the appointment!");

                await PopulateDoctors();
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private async Task PopulateDoctors()
        {
            Doctors = (await _userManager.GetUsersInRoleAsync(Constants.Roles.Doctor)).ToSelectList();
        }
    }
}
