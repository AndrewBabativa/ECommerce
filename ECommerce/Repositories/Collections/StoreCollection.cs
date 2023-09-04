using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ecommerce.Repositories.Collections
{
    public class StoreCollection : IStoreCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Store> Collection;

        public StoreCollection()
        {
            Collection = _repository.db.GetCollection<Store>("Stores");
        }

        public async Task DeleteStore(string id)
        {
            var filter = Builders<Store>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task InsertStore(Store Store)
        {
            await Collection.InsertOneAsync(Store);
        }

        public async Task UpdateStore(Store Store)
        {
            var filter = Builders<Store>.Filter.Eq(p => p.Id, Store.Id);
            await Collection.ReplaceOneAsync(filter, Store);
        }
        public async Task<List<Store>> GetAllStores()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Store> GetStoreById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }
    }
}
