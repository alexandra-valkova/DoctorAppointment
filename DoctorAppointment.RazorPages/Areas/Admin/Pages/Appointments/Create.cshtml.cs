using DoctorAppointment.Domain.Entities.Identity;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using DoctorAppointment.RazorPages.Areas.Admin.Models.Appointments;
using DoctorAppointment.RazorPages.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Constants = DoctorAppointment.Domain.Constants;

namespace DoctorAppointment.RazorPages.Areas.Admin.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentPolicy _appointmentPolicy;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IAppointmentService appointmentService, IAppointmentPolicy appointmentPolicy, UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _appointmentPolicy = appointmentPolicy;
            _userManager = userManager;
        }

        [BindProperty]
        public AppointmentWriteModel Appointment { get; set; } = default!;

        public SelectList Patients { get; set; } = default!;

        public SelectList Doctors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateSelectLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Appointment is null)
            {
                await PopulateSelectLists();
                return Page();
            }

            Result canCreateResult = await _appointmentPolicy.CanCreateAppointmentAsync(Appointment);

            if (canCreateResult.IsFailure)
            {
                ModelState.AddModelError(string.Empty, canCreateResult.Error.Description);

                await PopulateSelectLists();
                return Page();
            }

            try
            {
                await _appointmentService.CreateAppointmentAsync(Appointment);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the appointment!");

                await PopulateSelectLists();
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private async Task PopulateSelectLists()
        {
            Patients = (await _userManager.GetUsersInRoleAsync(Constants.Roles.Patient)).ToSelectList();
            Doctors = (await _userManager.GetUsersInRoleAsync(Constants.Roles.Doctor)).ToSelectList();
        }
    }
}
