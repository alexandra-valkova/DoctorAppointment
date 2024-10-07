using DoctorAppointment.Application.Features.Appointments.DTOs;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Errors;
using MediatR;

namespace DoctorAppointment.Application.Features.Appointments.Queries.List
{
    public sealed record ListAppointmentsQuery(AppointmentStatus? AppointmentStatus, int? PatientId, int? DoctorId) : IRequest<Result<List<AppointmentDTO>>>;
}
