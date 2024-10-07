using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;

namespace DoctorAppointment.RazorPages.Areas.Account.Models
{
    public class RegistrationModel : CredentialsModel
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public static implicit operator ApplicationUser(RegistrationModel model)
        {
            return new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
            };
        }
    }
}
