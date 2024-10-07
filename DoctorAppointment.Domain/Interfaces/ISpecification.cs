using System.Linq.Expressions;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Domain.Interfaces
{
    public interface ISpecification<T> where T : class, IEntity<int>
    {
        List<Expression<Func<T, bool>>> Filters { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>>? OrderBy { get; }

        Expression<Func<T, object>>? ThenOrderBy { get; }
    }
}
