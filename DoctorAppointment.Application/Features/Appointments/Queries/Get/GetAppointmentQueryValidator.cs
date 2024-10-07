using FluentValidation;

namespace DoctorAppointment.Application.Features.Appointments.Queries.Get
{
    public class GetAppointmentQueryValidator : AbstractValidator<GetAppointmentQuery>
    {
        public GetAppointmentQueryValidator()
        {
            RuleFor(request => request.Id).NotEmpty().GreaterThan(0);
        }
    }
}
