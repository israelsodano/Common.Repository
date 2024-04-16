using System.Linq.Expressions;

namespace Common.Repository
{
    public interface ICommonRepository<T>
        where T : Entity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Add(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void SaveChanges();
    }
}