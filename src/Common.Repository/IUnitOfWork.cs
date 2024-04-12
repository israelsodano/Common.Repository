namespace Common.Repository
{
    public interface IUnitOfWork
    {
        bool InTrasaction();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}