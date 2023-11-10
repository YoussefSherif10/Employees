using System.Linq.Expressions;
using Employees.Data;
using Employees.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly AppDbContext repositoryContext;

        protected RepositoryBase(AppDbContext RepositoryContext)
        {
            repositoryContext = RepositoryContext;
        }

        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);

        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(in bool trackChanges) =>
            trackChanges ? repositoryContext.Set<T>() : repositoryContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges
        ) =>
            trackChanges
                ? repositoryContext.Set<T>().Where(expression)
                : repositoryContext.Set<T>().Where(expression).AsNoTracking();

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);
    }
}
