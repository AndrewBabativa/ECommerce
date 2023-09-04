using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICategoryCollection
    {
        Task InsertCategory(Category Category);
        Task UpdateCategory(Category Category);
        Task DeleteCategory(string id);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(string id);
    }
}
