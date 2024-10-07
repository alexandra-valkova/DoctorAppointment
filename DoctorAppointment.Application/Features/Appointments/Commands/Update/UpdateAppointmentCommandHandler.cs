using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Update
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Result>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentPolicy _appointmentPolicy;

        public UpdateAppointmentCommandHandler(IAppointmentService appointmentService, IAppointmentPolicy appointmentPolicy)
        {
            _appointmentService = appointmentService;
            _appointmentPolicy = appointmentPolicy;
        }

        public async Task<Result> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Result canUpdateResult = await _appointmentPolicy.CanUpdateAppointmentAsync(request.Id, request.Appointment);

            if (canUpdateResult.IsSuccess)
            {
                request.Appointment.Id = request.Id;
                await _appointmentService.UpdateAppointmentAsync(request.Appointment);
                return Result.Success();
            }

            return Result.Failure(canUpdateResult.Error);
        }
    }
}
