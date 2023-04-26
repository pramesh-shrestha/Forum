using Application.DaoInterfaces;
using Application.Services.LogicInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace Application.Services.LogicImplementations; 

public class UserLogicImpl : IUserLogic {

    private readonly  IUserDao userDao;

    public UserLogicImpl(IUserDao userDao) {
        this.userDao = userDao;
    }

    public async Task<User> RegisterAsync(User user) {
        User? existing = await userDao.GetByUsernameAsync(user.Username);
        if (existing != null) {
            throw new Exception("Username already taken");
        }
        User created = await userDao.RegisterAsync(user);
        return created;
    }

    public async Task<User> ValidateUser(UserLoginDto loginDto) {
        User? existingUser = await userDao.GetByUsernameAsync(loginDto.Username);

        if (existingUser == null) {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(loginDto.Password)) {
            throw new Exception("Password does not match");
        }

        return existingUser;
    }
}