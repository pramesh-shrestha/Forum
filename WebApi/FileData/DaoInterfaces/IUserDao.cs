
using Shared.Models;

namespace WebApi.FileData.DaoInterfaces; 

public interface IUserDao {
    Task<User> RegisterAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
}