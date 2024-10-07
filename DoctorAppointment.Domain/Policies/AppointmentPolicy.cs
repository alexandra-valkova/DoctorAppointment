using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using static DoctorAppointment.Domain.Constants.Roles;

namespace DoctorAppointment.Domain.Policies
{
    public class AppointmentPolicy : IAppointmentPolicy
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserPolicy _userPolicy;

        public AppointmentPolicy(IAppointmentService appointmentService, IUserPolicy userPolicy)
        {
            _appointmentService = appointmentService;
            _userPolicy = userPolicy;
        }

        public async Task<Result> CanCreateAppointmentAsync(Appointment appointment)
        {
            return await CanSaveAppointment(appointment);
        }

        public async Task<Result> CanUpdateAppointmentAsync(int id, Appointment appointment)
        {
            if (await _appointmentService.GetAppointmentByIdAsync(id) is null)
            {
                return Result.Failure(AppointmentErrors.AppointmentNotFound(id));
            }

            return await CanSaveAppointment(appointment);
        }

        private async Task<Result> CanSaveAppointment(Appointment appointment)
        {
            Result patientResult = await _userPolicy.CheckIfUserInRoleExists(appointment.PatientId, Patient);

            if (patientResult.IsFailure) return patientResult;

            Result doctorResult = await _userPolicy.CheckIfUserInRoleExists(appointment.DoctorId, Doctor);

            if (doctorResult.IsFailure) return doctorResult;

            Result appointmentResult = await CheckIfAppointmentIsTaken(appointment);

            if (appointmentResult.IsFailure) return appointmentResult;

            return Result.Success();
        }

        private async Task<Result> CheckIfAppointmentIsTaken(Appointment appointment)
        {
            bool isAlreadyTaken = await _appointmentService.AppointmentExistsAsync(a => a.Id != appointment.Id
                                                                                        && a.DoctorId == appointment.DoctorId
                                                                                        && a.Date > appointment.Date.AddMinutes(-15)
                                                                                        && a.Date < appointment.Date.AddMinutes(15));

            return isAlreadyTaken
                ? Result.Failure(AppointmentErrors.AppointmentAlreadyTaken(appointment.Date))
                : Result.Success();
        }
    }
}
