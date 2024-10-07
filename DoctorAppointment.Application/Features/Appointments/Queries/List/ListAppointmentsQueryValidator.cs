using FluentValidation;

namespace DoctorAppointment.Application.Features.Appointments.Queries.List
{
    public class ListAppointmentsQueryValidator : AbstractValidator<ListAppointmentsQuery>
    {
        public ListAppointmentsQueryValidator()
        {
            RuleFor(request => request.AppointmentStatus).IsInEnum();
            RuleFor(request => request.PatientId).GreaterThan(0);
            RuleFor(request => request.DoctorId).GreaterThan(0);
        }
    }
}
