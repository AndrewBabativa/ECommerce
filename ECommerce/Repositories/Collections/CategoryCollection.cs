using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ecommerce.Repositories.Collections
{
    public class CategoryCollection : ICategoryCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Category> Collection;

        public CategoryCollection()
        {
            Collection = _repository.db.GetCollection<Category>("Categories");
        }

        public async Task DeleteCategory(string id)
        {
            var filter = Builders<Category>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task InsertCategory(Category Category)
        {
            await Collection.InsertOneAsync(Category);
        }

        public async Task UpdateCategory(Category Category)
        {
            var filter = Builders<Category>.Filter.Eq(p => p.Id, Category.Id);
            await Collection.ReplaceOneAsync(filter, Category);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }
    }
}
