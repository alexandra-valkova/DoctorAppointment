using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Policies;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Create
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Result<int>>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentPolicy _appointmentPolicy;

        public CreateAppointmentCommandHandler(IAppointmentService appointmentService, IAppointmentPolicy appointmentPolicy)
        {
            _appointmentService = appointmentService;
            _appointmentPolicy = appointmentPolicy;
        }

        public async Task<Result<int>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Result canCreateResult = await _appointmentPolicy.CanCreateAppointmentAsync(request.Appointment);

            if (canCreateResult.IsSuccess)
            {
                int createdId = await _appointmentService.CreateAppointmentAsync(request.Appointment);
                return Result.Success(createdId);
            }

            return Result.Failure<int>(canCreateResult.Error);
        }
    }
}
