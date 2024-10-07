using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.RazorPages.Attributes
{
    public sealed class MinDateToday : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return Convert.ToDateTime(value) > DateTime.Now
                ? ValidationResult.Success
                : new ValidationResult("Date must be equal to or greater than today!");
        }
    }
}
