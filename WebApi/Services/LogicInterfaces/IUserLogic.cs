
using Shared.Models;

namespace WebApi.Services.LogicInterfaces; 

public interface IUserLogic {
    Task<User> RegisterAsync(User user);
}