using FluentValidation;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Delete
{
    public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
    {
        public DeleteAppointmentCommandValidator()
        {
            RuleFor(request => request.Id).NotEmpty().GreaterThan(0);
        }
    }
}
