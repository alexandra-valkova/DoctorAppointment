namespace DoctorAppointment.Domain.Entities
{
    public abstract class Entity : IEntity<int>
    {
        public int Id { get; set; }
    }
}
