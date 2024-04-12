using System.Linq.Expressions;
using MongoDB.Driver;

namespace Common.Repository.MongoDB
{
    public sealed class MongoDBRepository<T>(IMongoClient client, 
                                             IMongoDatabase database,
                                             IClientSessionHandle session,
                                             MongoCollectionSettings cfg = null) : ICommonRepository<T>
        where T : Entity
    {
        private readonly IMongoClient _client = client;
        private readonly IMongoDatabase _database = database;
        private readonly IClientSessionHandle _session = session;
        private readonly IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name.ToLower(), cfg);
        private readonly IList<Action> _actions = [];
        
        public void Add(T entity) =>
            _actions.Add(() => 
                _collection.InsertOne(_session, entity));

        public void Add(IEnumerable<T> entities) =>
            _actions.Add(() => 
                _collection.InsertMany(_session, entities));

        public void Delete(T entity) =>
            _actions.Add(() => 
                _collection.DeleteOne(_session, x=> x.Id == entity.Id));

        public void Delete(IEnumerable<T> entities) =>
            _actions.Add(() => 
                _collection.DeleteMany(_session,
                    new FilterDefinitionBuilder<T>().In(x=> x.Id, entities.Select(y=> y.Id))));

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate) =>
            _collection.Find(predicate)
                       .ToList()
                       .AsQueryable();
        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate) =>
            _collection.Find(predicate).FirstOrDefault();

        public void Update(T entity) =>
            _actions.Add(() => 
                _collection.ReplaceOne(_session, x=> x.Id == entity.Id, entity));

        public void Update(IEnumerable<T> entities) =>
            _actions.Add(() => 
                entities.Select(e=> 
                    _collection.ReplaceOne(_session, x=> x.Id == e.Id, e)));

         public void SaveChanges() =>
            _actions.Select(x=> {
                x();
                return true;
            });
    }
}