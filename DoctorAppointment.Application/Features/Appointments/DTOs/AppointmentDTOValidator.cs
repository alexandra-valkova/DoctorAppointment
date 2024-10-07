using FluentValidation;

namespace DoctorAppointment.Application.Features.Appointments.DTOs
{
    public class AppointmentDTOValidator : AbstractValidator<AppointmentDTO>
    {
        public AppointmentDTOValidator()
        {
            RuleFor(appointment => appointment.PatientId).NotEmpty().GreaterThan(0)
                                                         .NotEqual(appointment => appointment.DoctorId)
                                                         .WithMessage(appointment => $"Appointment cannot have the same Id '{appointment.PatientId}' for both patient and doctor.");

            RuleFor(appointment => appointment.DoctorId).NotEmpty().GreaterThan(0);

            RuleFor(appointment => appointment.Date).NotEmpty().GreaterThan(DateTime.Now);

            RuleFor(appointment => appointment.Status).NotEmpty().IsInEnum();
        }
    }
}
