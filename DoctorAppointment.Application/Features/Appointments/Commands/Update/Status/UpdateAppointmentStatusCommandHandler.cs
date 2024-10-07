using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Update.Status
{
    public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, Result>
    {
        private readonly IAppointmentService _appointmentService;

        public UpdateAppointmentStatusCommandHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Result> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            bool isSuccess = await _appointmentService.UpdateAppointmentStatusAsync(request.Id, request.AppointmentStatus);

            return isSuccess ? Result.Success() : Result.Failure(AppointmentErrors.AppointmentNotFound(request.Id));
        }
    }
}
