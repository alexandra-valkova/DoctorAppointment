using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using DoctorAppointment.Domain.Interfaces.Services;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Queries.List
{
    public class ListAppointmentsQueryHandler : IRequestHandler<ListAppointmentsQuery, Result<List<AppointmentDTO>>>
    {
        private readonly IAppointmentService _appointmentService;

        public ListAppointmentsQueryHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Result<List<AppointmentDTO>>> Handle(ListAppointmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IReadOnlyList<Appointment> appointments = await _appointmentService.ListAppointmentsAsync(request.AppointmentStatus, request.PatientId, request.DoctorId);

                return Result.Success(appointments.Select(appointment => (AppointmentDTO)appointment).ToList());
            }
            catch (Exception)
            {
                return Result.Failure<List<AppointmentDTO>>(AppointmentErrors.AppointmentListNotFound);
            }
        }
    }
}
