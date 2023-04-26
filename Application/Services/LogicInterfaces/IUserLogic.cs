using Shared.Dtos;
using Shared.Models;

namespace Application.Services.LogicInterfaces; 

public interface IUserLogic {
    Task<User> RegisterAsync(User user);
    Task<User> ValidateUser(UserLoginDto loginDto);
}