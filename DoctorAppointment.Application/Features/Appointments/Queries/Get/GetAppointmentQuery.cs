using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Errors;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Queries.Get
{
    public sealed record GetAppointmentQuery(int Id) : IRequest<Result<AppointmentDTO>>;
}
