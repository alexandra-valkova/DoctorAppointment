namespace DoctorAppointment.RazorPages.Areas.Admin.Models.UserRoles
{
    public class UserRoleModel
    {
        public int? UserId { get; set; }

        public int? RoleId { get; set; }

        public string? Username { get; set; }

        public string? RoleName { get; set; }
    }
}
