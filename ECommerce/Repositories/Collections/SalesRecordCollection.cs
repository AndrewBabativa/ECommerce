using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ecommerce.Repositories.Collections
{
    public class SalesRecordCollection : ISalesRecordCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<SalesRecord> Collection;

        public SalesRecordCollection()
        {
            Collection = _repository.db.GetCollection<SalesRecord>("SalesRecords");
        }

        public async Task DeleteSalesRecord(string id)
        {
            var filter = Builders<SalesRecord>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task InsertSalesRecord(SalesRecord SalesRecord)
        {
            await Collection.InsertOneAsync(SalesRecord);
        }

        public async Task UpdateSalesRecord(SalesRecord SalesRecord)
        {
            var filter = Builders<SalesRecord>.Filter.Eq(p => p.Id, SalesRecord.Id);
            await Collection.ReplaceOneAsync(filter, SalesRecord);
        }
        public async Task<List<SalesRecord>> GetAllSalesRecords()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<SalesRecord> GetSalesRecordById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }
    }
}
