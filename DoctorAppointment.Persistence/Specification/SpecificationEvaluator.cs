using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.Specification
{
    public class SpecificationEvaluator<T> where T : class, IEntity<int>
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            IQueryable<T> query = inputQuery;

            if (specification.Filters.Count > 0)
            {
                query = specification.Filters.Aggregate(query, (current, filter) => current.Where(filter));
            }

            if (specification.Includes.Count > 0)
            {
                query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (specification.OrderBy is not null)
            {
                IOrderedQueryable<T> orderedQuery = query.OrderBy(specification.OrderBy);

                if (specification.ThenOrderBy is not null)
                {
                    orderedQuery = orderedQuery.ThenBy(specification.ThenOrderBy);
                }

                query = orderedQuery;
            }

            return query;
        }
    }
}
