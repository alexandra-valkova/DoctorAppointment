using DoctorAppointment.Domain.Entities;
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
    public class EditModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentPolicy _appointmentPolicy;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IAppointmentService appointmentService, IAppointmentPolicy appointmentPolicy, UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _appointmentPolicy = appointmentPolicy;
            _userManager = userManager;
        }

        [BindProperty]
        public AppointmentWriteModel Appointment { get; set; } = default!;

        public SelectList Patients { get; set; } = default!;

        public SelectList Doctors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue && await _appointmentService.GetAppointmentByIdAsync(id.Value) is Appointment appointment)
            {
                Appointment = appointment;

                await PopulateSelectLists();
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Appointment is null)
            {
                await PopulateSelectLists();
                return Page();
            }

            Result canEditResult = await _appointmentPolicy.CanUpdateAppointmentAsync(Appointment.Id, Appointment);

            if (canEditResult.IsFailure)
            {
                ModelState.AddModelError(string.Empty, canEditResult.Error.Description);

                await PopulateSelectLists();
                return Page();
            }

            try
            {
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
