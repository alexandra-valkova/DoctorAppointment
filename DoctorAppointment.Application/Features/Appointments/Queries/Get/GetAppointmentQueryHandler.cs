using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Queries.Get
{
    public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, Result<AppointmentDTO>>
    {
        private readonly IAppointmentService _appointmentService;

        public GetAppointmentQueryHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Result<AppointmentDTO>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            if (await _appointmentService.GetAppointmentByIdAsync(request.Id) is Appointment appointment)
            {
                return Result.Success((AppointmentDTO)appointment);
            }

            return Result.Failure<AppointmentDTO>(AppointmentErrors.AppointmentNotFound(request.Id));
        }
    }
}
