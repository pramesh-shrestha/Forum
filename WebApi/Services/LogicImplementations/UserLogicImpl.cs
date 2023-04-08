
using Shared.Models;
using WebApi.FileData.DaoInterfaces;
using WebApi.Services.LogicInterfaces;

namespace WebApi.Services.LogicImplementations; 

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
}