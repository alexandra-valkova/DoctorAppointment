using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Errors;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Create
{
    public sealed record CreateAppointmentCommand(AppointmentDTO Appointment) : IRequest<Result<int>>;
}
