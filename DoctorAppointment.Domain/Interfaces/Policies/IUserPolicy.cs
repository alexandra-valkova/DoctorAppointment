using DoctorAppointment.Domain.Errors;

namespace DoctorAppointment.Domain.Interfaces.Policies
{
    public interface IUserPolicy
    {
        Task<Result> CheckIfUserInRoleExists(int id, string role);
    }
}