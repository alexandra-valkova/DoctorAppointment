using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.RazorPages.Areas.Account.Models
{
    public class CredentialsModel
    {
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
