using DoctorAppointment.Application.Features.Appointments.DTOs;
using FluentValidation;

namespace DoctorAppointment.Application.Features.Appointments.Commands.Create
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(request => request.Appointment).SetValidator(new AppointmentDTOValidator());
        }
    }
}
