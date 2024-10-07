using System.ComponentModel.DataAnnotations;
using DoctorAppointment.Domain.Entities.Identity;

namespace DoctorAppointment.RazorPages.Areas.Admin.Models.Roles
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public static implicit operator RoleModel(ApplicationRole model)
        {
            return new RoleModel
            {
                Id = model.Id,
                Name = model.Name ?? string.Empty,
                Description = model.Description ?? string.Empty
            };
        }

        public static implicit operator ApplicationRole(RoleModel model)
        {
            return new ApplicationRole
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }
    }
}
