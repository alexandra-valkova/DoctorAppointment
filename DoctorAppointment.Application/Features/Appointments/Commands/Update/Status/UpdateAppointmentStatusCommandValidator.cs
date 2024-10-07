using FluentValidation;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Update.Status
{
    public class UpdateAppointmentStatusCommandValidator : AbstractValidator<UpdateAppointmentStatusCommand>
    {
        public UpdateAppointmentStatusCommandValidator()
        {
            RuleFor(request => request.Id).NotEmpty().GreaterThan(0);
            RuleFor(request => request.AppointmentStatus).NotNull().IsInEnum();
        }
    }
}
