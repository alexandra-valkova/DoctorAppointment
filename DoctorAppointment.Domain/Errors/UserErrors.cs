namespace DoctorAppointment.Domain.Errors
{
    public static class UserErrors
    {
        private const string Type = "User";

        public const string NotFound = "NotFound";
        public const string NotInRole = "NotInRole";

        public static Error UserNotFound(int id) => new(Type, NotFound, $"User with Id {id} cannot be found!");

        public static Error UserNotInRole(int id, string role) => new(Type, NotInRole, $"User with Id {id} is not a {role.ToLower()}!");
    }
}
