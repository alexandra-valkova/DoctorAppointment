using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Errors;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Delete
{
    public sealed record DeleteAppointmentCommand(int Id) : IRequest<Result<AppointmentDTO>>;
}
