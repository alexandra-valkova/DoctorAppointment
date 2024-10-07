namespace DoctorAppointment.Domain.Errors
{
    public static class AppointmentErrors
    {
        private const string Type = "Appointment";

        public const string NotFound = "NotFound";
        public const string AlreadyTaken = "AlreadyTaken";
        public const string ListNotFound = "ListNotFound";

        public static Error AppointmentNotFound(int id) => new(Type, NotFound, $"Appointment with Id {id} cannot be found!");

        public static Error AppointmentAlreadyTaken(DateTime date) => new(Type, AlreadyTaken, $"The appointment requested at {date} is already taken!");

        public static Error AppointmentListNotFound => new(Type, ListNotFound, "Couldn't retrieve list of appointments!");
    }
}
