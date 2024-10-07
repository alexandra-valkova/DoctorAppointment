using DoctorAppointment.Application.Features.Appointments.DTOs;
using FluentValidation;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Update
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(request => request.Id).NotEmpty().GreaterThan(0);
            RuleFor(request => request.Appointment).SetValidator(new AppointmentDTOValidator());
        }
    }
}
