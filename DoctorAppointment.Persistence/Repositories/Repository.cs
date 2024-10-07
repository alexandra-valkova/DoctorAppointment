using System.Linq.Expressions;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Interfaces;
using DoctorAppointment.Domain.Interfaces.Repositories;
using DoctorAppointment.Persistence.Context;
using DoctorAppointment.Persistence.Specification;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity<int>
    {
        protected readonly DoctorAppointmentContext dbContext;
        protected readonly DbSet<T> dbSet;

        protected Repository(DoctorAppointmentContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await dbSet.AnyAsync(entity => entity.Id == id);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<T?> GetByIdAsync(int id, ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<IReadOnlyList<T>> ListAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<int> AddAsync(T entity)
        {
            T createdEntity = (await dbSet.AddAsync(entity)).Entity;
            await SaveAsync();

            return createdEntity.Id;
        }

        public virtual async Task EditAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            T deletedEntity = dbSet.Remove(entity).Entity;
            await SaveAsync();

            return deletedEntity;
        }

        protected async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        protected IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(dbSet.AsNoTracking(), specification);
        }
    }
}
