using System.Linq.Expressions;

namespace Employees.Interfaces
{
    public interface IRepositoryBase<T>
    {
        public IQueryable<T> FindAll(in bool trackChanges);
        public IQueryable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges
        );
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
