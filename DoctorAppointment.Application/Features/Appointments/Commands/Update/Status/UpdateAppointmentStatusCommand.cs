using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Update.Status
{
    public sealed record UpdateAppointmentStatusCommand(int Id, AppointmentStatus AppointmentStatus) : IRequest<Result>;
}
