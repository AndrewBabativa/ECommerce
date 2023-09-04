using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface IUserCollection
    {
        Task InsertUser(User User);
        Task UpdateUser(User User);
        Task DeleteUser(string id);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<User> GetUserByUserName(string userName);
    }
}
