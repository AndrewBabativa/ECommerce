using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IProductCollection
    {
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(string id);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(string id);
    }
}
