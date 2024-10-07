using System.Linq.Expressions;
using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IEntity<int>
    {
        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByIdAsync(int id, ISpecification<T> specification);

        Task<IReadOnlyList<T>> ListAsync();

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);

        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate);

        Task<int> AddAsync(T entity);

        Task EditAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}
