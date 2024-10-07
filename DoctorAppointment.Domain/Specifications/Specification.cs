using System.Linq.Expressions;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces;

namespace DoctorAppointment.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T> where T : class, IEntity<int>
    {
        public List<Expression<Func<T, bool>>> Filters { get; } = [];

        public List<Expression<Func<T, object>>> Includes { get; } = [];

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? ThenOrderBy { get; private set; }

        protected void AddFilter(Expression<Func<T, bool>> filter)
        {
            Filters.Add(filter);
        }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        protected void SetOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void SetThenOrderBy(Expression<Func<T, object>> thenOrderBy)
        {
            ThenOrderBy = thenOrderBy;
        }
    }
}
