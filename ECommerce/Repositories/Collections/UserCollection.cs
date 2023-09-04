using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ecommerce.Repositories.Collections
{
    public class UserCollection : IUserCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<User> Collection;

        public UserCollection()
        {
            Collection = _repository.db.GetCollection<User>("Users");
        }

        public async Task DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task InsertUser(User User)
        {
            await Collection.InsertOneAsync(User);
        }

        public async Task UpdateUser(User User)
        {
            var filter = Builders<User>.Filter.Eq(p => p.Id, User.Id);
            await Collection.ReplaceOneAsync(filter, User);
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await Collection.Find(new BsonDocument { { "UserName", userName } }).FirstOrDefaultAsync();
        }
    }
}
