using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IStoreCollection
    {
        Task InsertStore(Store Store);
        Task UpdateStore(Store Store);
        Task DeleteStore(string id);
        Task<List<Store>> GetAllStores();
        Task<Store> GetStoreById(string id);
    }
}
