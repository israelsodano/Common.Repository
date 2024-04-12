using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Repository.SqlServer
{
    public class SqlServerUnitOfWork(DbContext dbContext) : ISqlServerUnitOfWork
    {
        private readonly DbContext _dbContext = dbContext;
        private IDbContextTransaction _dbContextTransaction;

        public void BeginTransaction()
        {
            if (_dbContextTransaction is not null)
                throw new NotSupportedException(Errors.ALREADY_INICIATED_TRANSACTION);
            _dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_dbContextTransaction is null)
                throw new NotSupportedException(Errors.NO_INICIATED_TRANSACTION);
            using (_dbContextTransaction)
                _dbContextTransaction.Commit();
            _dbContextTransaction = null;
        }

        public bool InTrasaction() =>
            _dbContextTransaction is not null;

        public void RollbackTransaction()
        {
            if (_dbContextTransaction is null)
                throw new NotSupportedException(Errors.NO_INICIATED_TRANSACTION);
            using (_dbContextTransaction)
                _dbContextTransaction.Rollback();
            _dbContextTransaction = null;
        }
    }
}