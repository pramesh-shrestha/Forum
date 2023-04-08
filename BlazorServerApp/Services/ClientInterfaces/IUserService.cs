
using Shared.Models;

namespace BlazorServerApp.Services.ClientInterfaces; 

public interface IUserService {
    Task<User> RegisterUser(User user);

}