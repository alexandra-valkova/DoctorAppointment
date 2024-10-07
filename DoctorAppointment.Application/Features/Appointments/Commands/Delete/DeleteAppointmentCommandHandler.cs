using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Delete
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Result<AppointmentDTO>>
    {
        private readonly IAppointmentService _appointmentService;

        public DeleteAppointmentCommandHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Result<AppointmentDTO>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (await _appointmentService.DeleteAppointmentAsync(request.Id) is Appointment appointment)
            {
                return Result.Success((AppointmentDTO)appointment);
            }

            return Result.Failure<AppointmentDTO>(AppointmentErrors.AppointmentNotFound(request.Id));
        }
    }
}
