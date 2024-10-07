using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Errors;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Update
{
    public sealed record UpdateAppointmentCommand(int Id, AppointmentDTO Appointment) : IRequest<Result>;
}
