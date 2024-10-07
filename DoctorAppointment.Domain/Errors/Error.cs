namespace DoctorAppointment.Domain.Errors
{
    public record Error(string Type, string Code, string Description)
    {
        public bool IsEmpty => this == None;

        public static readonly Error None = new(string.Empty, string.Empty, string.Empty);
    }
}
