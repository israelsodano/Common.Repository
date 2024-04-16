using MongoDB.Driver;

namespace Common.Repository.MongoDB
{
    internal sealed class MongoDBUnitOfWork(IMongoClient client,
                                            IClientSessionHandle session) : IMongoUnitOfWork
    {
        private readonly IMongoClient _client = client;
        private readonly List<Action> _actions = [];
        private readonly IClientSessionHandle _session = session;

        public void BeginTransaction()
        {
            if (_session.IsInTransaction)
                throw new NotSupportedException(Errors.ALREADY_INICIATED_TRANSACTION);
            _session.StartTransaction();
        }

        public void CommitTransaction()
        {
            using (_session)
                _session.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            using (_session)
                _session.AbortTransaction();
        }

        public bool InTrasaction() =>
            _session.IsInTransaction;
    }
}