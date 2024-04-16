using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Common.Repository.Sql
{
    internal sealed class SqlRepository<T>(DbContext dbContext) : ICommonRepository<T>
        where T : Entity
    {
        private readonly DbContext _dbContext = dbContext;
        private readonly DbSet<T> _dbset = dbContext.Set<T>();

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate) =>
            _dbset.Where(predicate)
                    .AsNoTracking();

        public IQueryable<T> GetAll() =>
            _dbset.AsNoTracking();

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate) =>
            _dbset.AsNoTracking()
                .FirstOrDefault(predicate);

        public void Add(T entity) =>
            _dbset.Add(entity);

        public void Add(IEnumerable<T> entities) =>
            _dbset.AddRange(entities);

        public void Delete(T entity) =>
            _dbset.Remove(entity);

        public void Delete(IEnumerable<T> entities) =>
            _dbset.RemoveRange(entities);

        public void Update(T entity) =>
            _dbset.Update(entity);

        public void Update(IEnumerable<T> entities) =>
            _dbset.UpdateRange(entities);

        public void SaveChanges() =>
            _dbContext.SaveChanges();
    }
}